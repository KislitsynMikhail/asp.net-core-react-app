import React, {useEffect, useState} from "react";
import {makeStyles} from "@material-ui/core/styles";
import {Typography, Button} from "@material-ui/core";
import DevicesTable from "./DevicesTable";
import DeviceForm, {emptyDevice} from "./DeviceForm";
import { connect } from "react-redux";
import {
    addDevice,
    deleteDevice,
    Device,
    editDevice,
    requestDevice,
    requestDevices
} from "../../redux/reducers/devices-reducer";
import {AppState} from "../../redux/store";
import {getCount, getDevice, getDevices, getError} from "../../redux/selectors/devices-selectors";
import SearchArea from "../common/SearchArea";
import { compose } from "redux";
import { withRouter, RouteComponentProps } from "react-router-dom";
import {Error} from "../../api/api"

const useStyles = makeStyles( {
    root: {
        marginTop: 60,
        marginLeft: 200,
        display: "flex",
    },
    container: {
        width: '100%',
        display: "flex",
        flexDirection: "column",
        marginTop: 20
    },
    title: {
        marginLeft: '3%',
        marginBottom: 10,
        fontSize: 20
    },
    content: {
        display: "flex",
        justifyContent: "space-between"
    },
    buttonAndSearchAreaContainer: {
        display: "flex",
    },
    button: {
        maxHeight: 35,
        minWidth: 20,
        marginLeft: '2.5%'
    }
})

type Props = MapStateToProps & MapDispatchToProps & {
    rowsPerPage: number
    page: number
    searchWord: string
    view: 'table' | 'form'
    addToFormUrl: (deviceId: number) => void
    addToTableUrl: (page: number, count: number, searchWord: string) => void
} & RouteComponentProps<PathParams>

const Devices: React.FC<Props> = ({
                                      devices, count, requestDevices, addToTableUrl, addToFormUrl,
                                      device, ...props}) => {
    const classes = useStyles()

    const [searchWord, setSearchWord] = useState(props.searchWord)
    const [page, setPage] = useState(props.page)
    const [rowsPerPage, setRowsPerPage] = useState(props.rowsPerPage)
    const [editMode, setEditMode] = useState(false)
    const [selectedDevice, setSelectedDevice] = useState<Device>(device)
    const [view, setView] = useState<'table' | 'form'>(props.view)
    useEffect(() => {
        setSearchWord(props.searchWord)
        setPage(props.page)
        setRowsPerPage(props.rowsPerPage)
        setView(props.view)
    }, [props.searchWord, props.page, props.rowsPerPage, props.view])

    let currentPage = props.page
    let currentRowsPerPage = props.rowsPerPage

    const requestNewDevices = (page: number, count: number, name: string) => {
        requestDevices(page, count, name)
        addToTableUrl(page, count, name)
    }

    const handleChangePage =  (newPage: number) => {
        setPage(newPage)
        currentPage = newPage
        requestNewDevices(newPage, currentRowsPerPage, searchWord)
    }

    const handleChangeRowsPerPage = (newRowsPerPage: number) => {
        setRowsPerPage(newRowsPerPage)
        currentRowsPerPage = newRowsPerPage
        requestNewDevices(currentPage, newRowsPerPage, searchWord)
    }

    const handleChangeSearchWord = (newSearchWord: string) => {
        setSearchWord(newSearchWord)
        const searchWord = newSearchWord.trim()
        addToTableUrl(page, rowsPerPage, newSearchWord)
        requestNewDevices(page, rowsPerPage, searchWord)
    }

    const onButtonClick = () => {
        setEditMode(false)
        setView('table')
        addToTableUrl(page, rowsPerPage, searchWord)
        setSelectedDevice(emptyDevice)
    }

    const handleAddButtonClick = (device: Device) => {
        props.addDevice(device)
        onButtonClick()
    }

    const handleCancelButtonClick = () => {
        onButtonClick()
    }

    const onRowClick = (id: number) => {
        const device = devices.find(device => device.id === id) as Device
        setSelectedDevice(device)
        setEditMode(true)
        setView("form")
        addToFormUrl(device.id)
    }

    const editDevice = (device: Device) => {
        props.editDevice(device)
        setView('table')
        addToTableUrl(page, rowsPerPage, searchWord)
    }

    const handleDeleteButtonClick = () => {
        props.deleteDevice(selectedDevice.id)
        setView('table')
        addToTableUrl(page, rowsPerPage, searchWord)
    }

    const handleButtonClick = () => {
        setView('form')
        addToFormUrl(0)
    }

    return (
        <div className={classes.root}>
            <div className={classes.container}>
                <Typography className={classes.title} >Устройства</Typography>
                { view === 'table' && <div>
                    <div className={classes.buttonAndSearchAreaContainer}>
                        <Button
                            className={classes.button}
                            type="button"
                            variant="contained"
                            color="primary"
                            onClick={handleButtonClick}
                        >
                            Добавить
                        </Button>
                        <SearchArea searchWord={searchWord} setSearchWord={handleChangeSearchWord} />
                    </div>
                    <DevicesTable
                        rowsPerPage={rowsPerPage}
                        onRowClick={onRowClick}
                        count={count}
                        setRowsPerPage={handleChangeRowsPerPage}
                        setPage={handleChangePage}
                        devices={devices}
                        page={page}
                    />
                </div>}
                {view === 'form' && <DeviceForm
                    initDevice={selectedDevice}
                    submitButtonLabel={editMode ? 'Изменить' : 'Добавить'}
                    secondButtonLabel={'Отменить'}
                    onSubmitButtonClick={editMode ? editDevice : handleAddButtonClick}
                    onSecondButtonClick={handleCancelButtonClick}
                    onDeleteButtonClick={editMode ? handleDeleteButtonClick : undefined}
                />}
            </div>
        </div>
    )
}

type MapStateToProps = {
    device: Device
    devices: Array<Device>
    count: number
    error: Error
}

type MapDispatchToProps = {
    requestDevice: (id: number) => void
    requestDevices: (page: number, count: number, name: string) => void
    addDevice: (device: Device) => void
    editDevice: (device: Device) => void
    deleteDevice: (id: number) => void
}

type PathParams = {
    view: string
}

type PropsContainer = MapStateToProps & MapDispatchToProps & RouteComponentProps<PathParams>
type State = {
    view: 'table' | 'form'
    page: number
    count: number
    searchWord: string
    deviceId: number
}
class DevicesContainer extends React.Component<PropsContainer, State> {
    constructor(props: PropsContainer) {
        super(props);

        this.addToUrl = this.addToUrl.bind(this)
        this.addToFormUrl = this.addToFormUrl.bind(this)
        this.addToTableUrl = this.addToTableUrl.bind(this)

        const params = props.match.params
        const view = params.view === 'form' ? 'form' : 'table'
        const queryString = new URLSearchParams(props.location.search)
        const pagePath = queryString.get('page')
        const page = pagePath && /^\d+$/.test(pagePath) ? Number(pagePath) : 0
        const countPath = queryString.get('count')
        let count = countPath && /^\d+$/.test(countPath) ? Number(countPath) : 10
        if (count !== 10 && count !== 25 && count !== 100) {
            count = 10
        }
        const searchPath = queryString.get('search')
        const searchWord = searchPath ? searchPath : ''
        const deviceIdPath = queryString.get('deviceId')
        const deviceId = deviceIdPath && /^\d+$/.test(deviceIdPath) ? Number(deviceIdPath) : 0
        this.state = {
            view,
            count,
            searchWord,
            page,
            deviceId
        }

        const path = view === 'form'
            ? `${deviceId && deviceId !== 0 ? `?deviceId=${deviceId}` : ''}`
            : `?page=${page}&count=${count}${searchWord ? `&search=${searchWord}` : ''}`
        this.addToUrl(`${view}${path}`)
    }

    addToUrl(path: string) {
        this.props.history.location.pathname = '/'

        this.props.history.replace(`devices/${path}`)
    }

    addToFormUrl(deviceId: number) {
        const path = `form${deviceId && deviceId !== 0 ? `?deviceId=${deviceId}` : ''}`
        this.addToUrl(path)
    }

    addToTableUrl(page: number, count: number, searchWord: string) {
        const path = `table?page=${page}&count=${count}${searchWord ? `&search=${searchWord}` : ''}`
        this.addToUrl(path)
    }

    componentDidMount() {
        this.props.requestDevices(this.state.page, this.state.count, this.state.searchWord)
        if (this.state.deviceId !== 0) {
            this.props.requestDevice(this.state.deviceId)
        }
    }

    render() {

        if (this.props.error.title === 'Not found' && this.state.deviceId !== 0) {
            this.addToUrl(this.state.view)
        }

        return (
            <Devices
                devices={this.props.devices}
                count={this.props.count}
                requestDevices={this.props.requestDevices}
                editDevice={this.props.editDevice}
                deleteDevice={this.props.deleteDevice}
                addDevice={this.props.addDevice}
                page={this.state.page}
                rowsPerPage={this.state.count}
                searchWord={this.state.searchWord}
                view={this.state.view}
                addToTableUrl={this.addToTableUrl}
                addToFormUrl={this.addToFormUrl}
                device={this.props.device}
                {...this.props}
            />
        )
    }
}

const mapStateToProps = (state: AppState): MapStateToProps => ({
    device: getDevice(state),
    devices: getDevices(state),
    count: getCount(state),
    error: getError(state)
})

export default compose<React.ComponentType>(
    connect(mapStateToProps, {requestDevices, editDevice, deleteDevice, addDevice, requestDevice}),
    withRouter
    )(DevicesContainer)