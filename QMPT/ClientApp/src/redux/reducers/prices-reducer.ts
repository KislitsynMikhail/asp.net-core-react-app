import {AppState, BaseThunk, InferAction} from "../store";
import {AxiosResponse} from "axios";
import {Dispatch} from "react";
import {getError} from "../selectors/organizations-selectors";
import {emptyError, StatusCodes, Error} from "../../api/api";
import {reExecute} from "../../App";
import {pricesAPI} from "../../api/prices-api";
import {emptyPriceValues} from "../../components/Price/PriceAddEditForm";

const initialState = {
    price: emptyPriceValues as Price,
    prices: [] as Array<Price>,
    pricesCount: 0,
    error: emptyError
}

const pricesReducer = (state = initialState, action: Action): InitialState => {
    switch (action.type) {
        case "prices/setError":
            return {
                ...state,
                error: action.error
            }
        case "prices/setPrices":
            return {
                ...state,
                prices: action.prices
            }
        case "prices/addPrice":
            return {
                ...state,
                prices: [action.price, ...state.prices]
            }
        case "prices/editPrice":
            const priceIndex = state.prices.findIndex(val => val.id === action.price.id)
            return {
                ...state,
                prices: [...state.prices.slice(0, priceIndex), action.price, ...state.prices.slice(priceIndex + 1)]
            }
        case "prices/deletePrice":
            return {
                ...state,
                prices: state.prices.filter(val => val.id !== action.priceId)
            }
        case "prices/setCount":
            return {
                ...state,
                pricesCount: action.count
            }
        case "prices/setPrice":
            return {
                ...state,
                price: action.price
            }
        default:
            return state
    }
}

export const actions = {
    setError: (error: Error) => ({type: 'prices/setError', error} as const),
    clearError: () => actions.setError(emptyError),
    setPrices: (prices: Array<Price>) => ({type: 'prices/setPrices', prices} as const),
    addPrice: (price: Price) => ({type: 'prices/addPrice', price} as const),
    editPrice: (price: Price) => ({type: 'prices/editPrice', price} as const),
    deletePrice: (priceId: number) => ({type: 'prices/deletePrice', priceId} as const),
    setCount: (count: number) => ({type: 'prices/setCount', count} as const),
    setPrice: (price: Price) => ({type: 'prices/setPrice', price} as const)
}

export const requestPrices = (type: PriceType, page: number, count: number, name: string): Thunk => {
    return async (dispatch, getState) => {
        const requestPrices = async (type: PriceType, page: number, count: number, name: string) => {
            try {
                const pricesResponse = await pricesAPI.getPrices(type, page, count, name)
                const prices = pricesResponse.data as Prices

                dispatch(actions.setPrices(prices.prices))
                dispatch(actions.setCount(prices.count))
                dispatch(actions.clearError())
            } catch (error) {
                handleUnauthorizedRequest(error.response, getState(), dispatch,
                    () => requestPrices(type, page, count, name))
            }
        }
        await requestPrices(type, page, count, name)
    }
}

export const requestPrice = (id: number): Thunk => {
    return async (dispatch, getState) => {
        const requestPrice = async (id: number) => {
            try {
                debugger
                const priceResponse = await pricesAPI.getPrice(id)
                const price = priceResponse.data as Price

                dispatch(actions.setPrice(price))
                dispatch(actions.clearError())
            } catch (error) {
                handleUnauthorizedRequest(error.response, getState(), dispatch,
                    () => requestPrice(id))
            }
        }
        await requestPrice(id)
    }
}

export const addPrice = (price: Price): Thunk => {
    return async (dispatch, getState) => {
        const state = getState()
        const addPrice = async (price: Price) => {
            try {
                const priceResponse = await pricesAPI.postPrice(price)
                price = priceResponse.data as Price

                dispatch(actions.addPrice(price))
                dispatch(actions.setCount(state.pricesState.pricesCount + 1))
                dispatch(actions.clearError())
            } catch (error) {
                handleUnauthorizedRequest(error.response, state, dispatch,
                    () => addPrice(price))
            }
        }
        await addPrice(price)
    }
}

export const editPrice = (price: Price): Thunk => {
    return async (dispatch, getState) => {
        const editPrice = async (price: Price) => {
            try {
                const priceResponse = await pricesAPI.patchPrice(price)
                price = priceResponse.data as Price

                dispatch(actions.editPrice(price))
                dispatch(actions.clearError())
            } catch (error) {
                handleUnauthorizedRequest(error.response, getState(), dispatch,
                    () => editPrice(price))
            }
        }
        await editPrice(price)
    }
}

export const deletePrice = (priceId: number): Thunk => {
    return async (dispatch, getState) => {
        const state = getState()
        const deletePrice = async (priceId: number) => {
            try {
                await pricesAPI.deletePrice(priceId)

                dispatch(actions.deletePrice(priceId))
                dispatch(actions.setCount(state.pricesState.pricesCount - 1))
                dispatch(actions.clearError())
            } catch (error) {
                handleUnauthorizedRequest(error.response, state, dispatch,
                    () => deletePrice(priceId))
            }
        }
        await deletePrice(priceId)
    }
}

const handleUnauthorizedRequest = (response: AxiosResponse, state: AppState, dispatch: Dispatch<Action>, method: () => void) => {
    if (response === undefined) {
        return
    }
    const error = getError(state)
    dispatch(actions.setError(response.data))
    if (response.status === StatusCodes.Unauthorized && error === emptyError) {
        reExecute(method)
    }
}

export default pricesReducer

export type Price = {
    id: number
    name: string
    cost: string
    type: PriceType
}
export type PriceType = 'Repair' | 'Delivery'
export type Prices = {
    prices: Array<Price>
    count: number
}
export type InitialState = typeof initialState
type Action = InferAction<typeof actions>
type Thunk = BaseThunk<Action>