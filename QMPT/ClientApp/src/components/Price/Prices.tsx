import React, {useEffect, useState} from "react";
import {Typography, FormControl, RadioGroup, FormControlLabel, Radio} from "@material-ui/core";
import {makeStyles} from "@material-ui/core/styles";
import PriceTable from "./PriceTable";
import {
    addPrice,
    deletePrice,
    editPrice,
    Price,
    PriceType,
    requestPrice,
    requestPrices
} from "../../redux/reducers/prices-reducer";
import {AppState} from "../../redux/store";
import {getCount, getError, getPrice, getPrices} from "../../redux/selectors/prices-selectors";
import {connect} from "react-redux";
import PriceAddEditForm, {emptyPriceValues, PriceValues} from "./PriceAddEditForm";
import SearchArea from "../common/SearchArea";
import { compose } from "redux";
import {RouteComponentProps, withRouter} from "react-router-dom";
import {Error} from "../../api/api"

const useStyles = makeStyles({
    root: {
        marginTop: 80,
        marginLeft: 200,
        display: "flex",
    },
    container: {
        width: '100%',
    },
    content: {
        display: "flex",
        justifyContent: "space-between"
    },
    aboveTable: {
        display: "flex"
    },
    title: {
        marginLeft: '3%',
        marginBottom: 10,
        fontSize: 20
    }
})

const emptyErrors = {
    name: '',
    cost: ''
}

export type Errors = typeof emptyErrors

type Props = {
    price: PriceValues
    prices: Array<Price>
    count: number
    initPage: number
    initRowsPerPage: number
    initSearchWord: string
    type: PriceType
    initIsEditMode: boolean
    addToUrl: (type: PriceType, page: number, count: number, search: string, priceId: number) => void
} & MapDispatchToProps

const Prices: React.FC<Props> = ({
                                     prices, count, requestPrices, addPrice, editPrice, deletePrice,
                                     addToUrl, price, initSearchWord, initPage, type,
                                     initRowsPerPage, initIsEditMode}) => {
    const classes = useStyles()

    const [searchWord, setSearchWord] = useState(initSearchWord)
    const [page, setPage] = useState(initPage)
    const [rowsPerPage, setRowsPerPage] = useState(initRowsPerPage)
    const [priceType, setPriceType] = useState<PriceType>(type)
    const [isEditMode, setIsEditMode] = useState(initIsEditMode)
    const [priceValues, setPriceValues] = useState(price)
    const [errors, setErrors] = useState(emptyErrors)

    useEffect(() => {
        setSearchWord(initSearchWord)
        setPage(initPage)
        setRowsPerPage(initRowsPerPage)
        setPriceType(type)
        setPriceValues(price)
        setIsEditMode(initIsEditMode)
    }, [initSearchWord, initPage, initRowsPerPage, type, price, initIsEditMode])

    let currentPage = initPage
    let currentRowsPerPage = initRowsPerPage

    if (prices.length > rowsPerPage) {
        prices = prices.slice(0, rowsPerPage)
    }

    const requestNewPrices = (priceType: PriceType, page: number, rowsPerPage: number, searchWord: string) => {
        clearPriceValues()
        clearErrors()
        requestPrices(priceType, page, rowsPerPage, searchWord)
        addToUrl(priceType, page, rowsPerPage, searchWord, 0)
    }

    const handleChangePage =  (newPage: number) => {
        setPage(newPage)
        currentPage = newPage
        requestNewPrices(priceType, newPage, currentRowsPerPage, searchWord)
    }

    const handleChangeRowsPerPage = (newRowsPerPage: number) => {
        setRowsPerPage(newRowsPerPage)
        currentRowsPerPage = newRowsPerPage
        requestNewPrices(priceType, currentPage, newRowsPerPage, searchWord)
    }

    const handleChangeSearchWord = (newSearchWord: string) => {
        setSearchWord(newSearchWord)
        const searchWord = newSearchWord.trim()
        requestNewPrices(priceType, page, rowsPerPage, searchWord)
    }

    const handleRowClick = (priceId: number) => {
        const price = prices.find(val => val.id === priceId) as PriceValues
        addToUrl(priceType, page, rowsPerPage, searchWord, price.id)
        setPriceValues(price)
        setIsEditMode(true)
        clearErrors()
    }

    const handleCancelButtonClick = () => {
        setIsEditMode(false)
        clearPriceValues()
        clearErrors()
    }

    const handleChangeRadioValue = (event: React.ChangeEvent<HTMLInputElement>) => {
        const priceType = (event.target as HTMLInputElement).value as PriceType
        setPriceType(priceType)
        setIsEditMode(false)
        clearPriceValues()
        requestNewPrices(priceType, page, rowsPerPage, searchWord)
    }

    const onSubmitButtonClick = () => {
        const price = {...priceValues, type: priceType}
        if (isEditMode) {
            setIsEditMode(false)
            const foundPrice = prices.find(val => val.id === price.id) as Price
            if (foundPrice.name !== price.name || foundPrice.cost !== price.cost) {
                editPrice(price)
            }
        } else {
            addPrice(price)
        }

        clearPriceValues()
    }

    const onDeleteButtonClick = () => {
        deletePrice(priceValues.id)
        handleCancelButtonClick()
        requestNewPrices(priceType, page, count, searchWord)
    }

    const clearPriceValues = () => {
        setPriceValues(emptyPriceValues)
        addToUrl(priceType, page, rowsPerPage, searchWord, 0)
    }

    const clearErrors = () => {
        setErrors(emptyErrors)
    }

    return (
        <div className={classes.root}>
            <div className={classes.container}>
                <Typography className={classes.title} >Прайс</Typography>
                <div className={classes.aboveTable}>
                    <FormControl className={classes.title} component="fieldset">
                        <RadioGroup row value={priceType} onChange={handleChangeRadioValue}>
                            <FormControlLabel value="Repair" control={<Radio />} label="Ремонт" />
                            <FormControlLabel value="Delivery" control={<Radio />} label="Поставка" />
                        </RadioGroup>
                    </FormControl>
                    <SearchArea searchWord={searchWord} setSearchWord={handleChangeSearchWord} />
                </div>
                <div className={classes.content}>
                    <PriceTable
                        onRowClick={handleRowClick}
                        page={page}
                        setPage={handleChangePage}
                        rowsPerPage={rowsPerPage}
                        setRowsPerPage={handleChangeRowsPerPage}
                        prices={prices}
                        count={count}
                    />
                    <PriceAddEditForm
                        values={priceValues}
                        setValues={setPriceValues}
                        isEditMode={isEditMode}
                        handleCancelButtonClick={handleCancelButtonClick}
                        handleSubmit={onSubmitButtonClick}
                        errors={errors}
                        setErrors={setErrors}
                        handleDeleteButtonClick={onDeleteButtonClick}
                    />
                </div>
            </div>
        </div>
    )
}
type PathParams = {
    type: string
    page: string
    count: string
    search: string
    priceId: string
}
type PropsContainer = MapStateToProps & MapDispatchToProps & RouteComponentProps<PathParams>
type State = {
    type: PriceType
    priceId: number
    page: number
    count: number
    searchWord: string
}

class PricesContainer extends React.Component<PropsContainer, State> {
    constructor(props: PropsContainer) {
        super(props);

        this.addToUrl = this.addToUrl.bind(this)

        const queryString = new URLSearchParams(props.location.search)
        const typePath = queryString.get('type')
        const type = typePath && typePath.toLowerCase() === 'delivery' ? 'Delivery' : 'Repair'
        const pagePath = queryString.get('page')
        const page = pagePath && /^\d+$/.test(pagePath) ? Number(pagePath) : 0
        const countPath = queryString.get('count')
        let count = countPath && /^\d+$/.test(countPath) ? Number(countPath) : 10
        if (count !== 10 && count !== 25 && count !== 100) {
            count = 10
        }
        const searchPath = queryString.get('search')
        const search = searchPath ? searchPath : ''
        const priceIdPath = queryString.get('priceId')
        const priceId = priceIdPath && /^\d+$/.test(priceIdPath) ? Number(priceIdPath) : 0

        this.addToUrl(type, page, count, search, priceId)
        this.state = {
            type,
            page,
            count,
            searchWord: search,
            priceId
        }
    }

    addToUrl(type: PriceType, page: number, count: number, search: string, priceId: number) {
        this.props.history.location.pathname = '/'

        const path = `price?type=${type.toLowerCase()}&page=${page}&count=${count}${search ? `&=search=${search}` : ''}${priceId !== 0 ? `&priceId=${priceId}` : ''}`
        this.props.history.replace(path)
    }

    componentDidMount() {
        debugger
        this.props.requestPrices(this.state.type, this.state.page, this.state.count, this.state.searchWord)
        if (this.state.priceId !== 0) {
            this.props.requestPrice(this.state.priceId)
        }
    }

    render() {

        if (this.props.error.title === 'Not found' && this.state.priceId !== 0) {
            this.addToUrl(this.state.type, this.state.page, this.state.count, this.state.searchWord, 0)
        }

        const price = this.props.price ? this.props.price : emptyPriceValues

        return (
            <Prices
                price={price}
                prices={this.props.prices}
                count={this.props.count}
                requestPrices={this.props.requestPrices}
                addPrice={this.props.addPrice}
                editPrice={this.props.editPrice}
                deletePrice={this.props.deletePrice}
                addToUrl={this.addToUrl}
                initPage={this.state.page}
                initRowsPerPage={this.state.count}
                initSearchWord={this.state.searchWord}
                type={this.state.type}
                initIsEditMode={price !== emptyPriceValues}
                {...this.props}
            />
        )
    }
}
type MapDispatchToProps = {
    requestPrices: (type: PriceType, page: number, count: number, name: string) => void
    addPrice: (price: Price) => void
    editPrice: (price: Price) => void
    deletePrice: (priceId: number) => void
    requestPrice: (id: number) => void
}
type MapStateToProps = {
    prices: Array<Price>
    count: number
    price: Price
    error: Error
}
const mapStateToProps = (state: AppState): MapStateToProps => ({
    prices: getPrices(state),
    count: getCount(state),
    price: getPrice(state),
    error: getError(state)
})

export default compose<React.ComponentType>(
    connect(mapStateToProps, {requestPrices, addPrice, editPrice, deletePrice, requestPrice}),
    withRouter
    )(PricesContainer)