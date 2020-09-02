import React, {useEffect, useState} from "react";
import { makeStyles } from '@material-ui/core/styles';
import OrganizationAddEditForm, { AddFormData, constInitialValues } from "./OrganizationAddEditForm";
import OrganizationsTable from "./OrgnaizationsTable";
import SearchArea from "../common/SearchArea";
import Button from "@material-ui/core/Button";
import clxs from 'classnames'
import {connect} from "react-redux";
import {AppState} from "../../redux/store";
import OrganizationDialog from "./OrganizationDialog"
import {
    getOrganizationsCount,
    getOrganizations
} from "../../redux/selectors/organizations-selectors";
import {
    Organization,
    OrganizationsTypes,
    requestOrganizations,
    clearContactPeople, addOrganization, clearOrganizations
} from "../../redux/reducers/organizations-reducer";
import {Typography} from "@material-ui/core";
import { compose } from "redux";
import {RouteComponentProps, withRouter} from "react-router-dom";

const useStyles = makeStyles({
    root: {
        marginTop: 80,
        marginLeft: 200,
        display: "flex",
        flexDirection: "column",
    },
    button: {
        marginLeft: '2.5%',
        marginBottom: 10
    },
    container: {
        width: '100%'
    },
    topBlock: {
        display: "flex",
    },
    topBlockInColumn: {
        display: "flex",
        flexDirection: "column"
    },
    title: {
        marginLeft: '3%',
        marginBottom: 10,
        fontSize: 20
    }
})

type MapStateToProps = {
    organizations: Array<Organization>
    count: number
}
type MapDispatchToProps = {
    requestOrganizations: (organizationType: OrganizationsTypes, page: number, count: number, name?: string) => void
    addOrganization: (organization: Organization) => void
    clearContactPeople: () => void
    clearOrganizations: () => void
}
type OwnProps = {
    organizationType: OrganizationsTypes
}
type Props = MapStateToProps & MapDispatchToProps & OwnProps
type OrganizationsProps = {
    addToTableUrl: (page: number, count: number, searchWord: string) => void
    addToContactsUrl: (organizationId: number) => void
    organizationId: number
    initSearchWord: string
    initPage: number
    initRowsPerPage: number
    initView: 'table' | 'contacts'
} & Props

const Organizations: React.FC<OrganizationsProps> = ({
                                                         organizations, count, requestOrganizations,
                                                         organizationType, clearContactPeople,
                                                         addOrganization, organizationId, initSearchWord,
                                                         initPage, initRowsPerPage, initView,
                                                         addToTableUrl, addToContactsUrl}) => {
    const classes = useStyles()
    const [isAdding, setIsAdding] = useState(false)
    const [searchWord, setSearchWord] = useState(initSearchWord)
    const [view, setView] = useState(initView)
    const [selectedOrganizationId, setSelectedOrganizationId] = useState(organizationId)
    const [page, setPage] = useState(initPage)
    const [rowsPerPage, setRowsPerPage] = useState(initRowsPerPage)
    useEffect(() => {
        setSelectedOrganizationId(organizationId)
        setSearchWord(initSearchWord)
        setPage(initPage)
        setRowsPerPage(initRowsPerPage)
        setView(initView)
    }, [organizationId, initSearchWord, initPage, initRowsPerPage, initView])

    let currentPage = initPage
    let currentRowsPerPage = initRowsPerPage

    if (organizations.length > rowsPerPage) {
        organizations = organizations.slice(0, rowsPerPage)
    }

    const requestNewOrganizations = (organizationType: OrganizationsTypes, page: number, count: number, name: string) => {
        requestOrganizations(organizationType, page, count, name)
        addToTableUrl(page, count, name)
    }

    const onChangePage =  (newPage: number) => {
        setPage(newPage)
        currentPage = newPage
        requestNewOrganizations(organizationType, newPage, currentRowsPerPage, searchWord)
    }

    const onChangeRowsPerPage = (newRowsPerPage: number) => {
        setRowsPerPage(newRowsPerPage)
        currentRowsPerPage = newRowsPerPage
        requestOrganizations(organizationType, currentPage, newRowsPerPage, searchWord)
    }

    const onChangeSearchWord = (newSearchWord: string) => {
        setSearchWord(newSearchWord)
        const searchWord = newSearchWord.trim()
        requestNewOrganizations(organizationType, page, rowsPerPage, searchWord)
    }

    const onAddButtonClick = (organization: AddFormData) => {
        addOrganization({...organization, organizationType})
        setIsAdding(false)
    }

    const onAddFormButtonClick = () => {
        setIsAdding(true)
    }

    const onCancelButtonClick = () => {
        setIsAdding(false)
    }

    const turnOffDialogMode = () => {
        setView('table')
        requestNewOrganizations(organizationType, page, rowsPerPage, searchWord)
        clearContactPeople()
    }

    const getOrganizationValues = (): Organization => {
        const row = organizations.find(organization => organization.id === selectedOrganizationId)

        if (row === undefined)
            return {...constInitialValues, organizationType}

        return row
    }

    const getTitle = () => {
        if (organizationType === 'Customer')
            return 'Заказчики'
        else if (organizationType === 'Provider')
            return 'Поставщики'
        else return ''
    }

    const showContacts = (organizationId: number) => {
        setView('contacts')
        setSelectedOrganizationId(organizationId)
        addToContactsUrl(organizationId)
    }

    return (
        <div className={classes.root}>
            <div className={classes.container}>
                <Typography className={classes.title} >{getTitle()}</Typography>
                <div className={clxs({[classes.topBlock]: !isAdding,[classes.topBlockInColumn]: isAdding })}>
                    { !isAdding &&
                    <Button className={classes.button} onClick={onAddFormButtonClick} variant="contained" color="primary">
                        Добавить
                    </Button> }
                    { isAdding &&
                    <OrganizationAddEditForm
                        onEditOrAddButtonClick={onAddButtonClick}
                        onCancelButtonClick={onCancelButtonClick}
                        buttonNameOnEditOrAddingMode={'Добавить'}
                        isEditOrAddingMode={true}
                    />}
                    <SearchArea searchWord={searchWord} setSearchWord={onChangeSearchWord}/>
                </div>
                <OrganizationsTable
                    showContacts={showContacts}
                    page={page}
                    setPage={onChangePage}
                    rowsPerPage={rowsPerPage}
                    setRowsPerPage={onChangeRowsPerPage}
                    organizations={organizations}
                    count={count}
                />
                <OrganizationDialog
                    organization={getOrganizationValues()}
                    isOpen={view === 'contacts'}
                    closeDialog={turnOffDialogMode}
                />
            </div>
        </div>
    );
}

type PathParams = {
    view: string
    organizationId: string
}

type ContainerProps = Props & RouteComponentProps<PathParams>
type State = {
    view: 'table' | 'contacts'
    page: number
    count: number
    searchWord: string
    organizationId: number
}
class OrganizationsContainer extends React.Component<ContainerProps, State> {
    constructor(props: ContainerProps) {
        super(props);

        this.addToUrl = this.addToUrl.bind(this)
        this.addToContactsUrl = this.addToContactsUrl.bind(this)
        this.addToTableUrl = this.addToTableUrl.bind(this)

        const params = props.match.params
        const view = params.view === 'contacts' ? 'contacts' : 'table'
        const organizationId = params.organizationId && /^\d+$/.test(params.organizationId) ? Number(params.organizationId) : 0
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
        this.state = {
            view: organizationId !== 0 ? view : "table",
            count,
            searchWord,
            page,
            organizationId
        }

        let path = `${this.state.view}`
        path += this.state.view === "contacts"
            ? `/${organizationId}`
            : `?page=${page}&count=${count}${searchWord ? `&search=${searchWord}` : ''}`
        this.addToUrl(path)
    }

    addToUrl(path: string) {
        this.props.history.location.pathname = '/'

        this.props.history.replace(`${this.props.organizationType.toLowerCase()}s/${path}`)
    }

    addToContactsUrl(organizationId: number) {
        const path = `contacts/${organizationId}`
        this.addToUrl(path)
    }

    addToTableUrl(page: number, count: number, searchWord: string) {
        const path = `table?page=${page}&count=${count}${searchWord ? `&search=${searchWord}` : ''}`
        this.addToUrl(path)
    }

    componentDidMount() {
        this.requestOrganizations()
    }

    componentDidUpdate(prevProps: Readonly<Props>, prevState: Readonly<{}>, snapshot?: any) {
        if (prevProps.organizationType !== this.props.organizationType) {
            this.requestOrganizations()
        }
    }

    requestOrganizations() {
        this.props.requestOrganizations(this.props.organizationType, this.state.page, this.state.count, this.state.searchWord)
    }

    render() {
        return <Organizations
            initView={this.state.view}
            organizationId={this.state.organizationId}
            initSearchWord={this.state.searchWord}
            initPage={this.state.page}
            initRowsPerPage={this.state.count}
            addToContactsUrl={this.addToContactsUrl}
            addToTableUrl={this.addToTableUrl}
            organizationType={this.props.organizationType}
            count={this.props.count}
            organizations={this.props.organizations}
            requestOrganizations={this.props.requestOrganizations}
            clearContactPeople={this.props.clearContactPeople}
            addOrganization={this.props.addOrganization}
            clearOrganizations={this.props.clearOrganizations}
         />
    }
}

const mapStateToProps = (state: AppState): MapStateToProps => ({
    organizations: getOrganizations(state),
    count: getOrganizationsCount(state)
})
export default compose<React.ComponentType<OwnProps>>(
    connect(mapStateToProps, {requestOrganizations, clearContactPeople, addOrganization, clearOrganizations}),
    withRouter)
(OrganizationsContainer)