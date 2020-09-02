import {AppState} from "../store";

export const getPrices = (state: AppState) => (
    state.pricesState.prices
)

export const getCount = (state: AppState) => (
    state.pricesState.pricesCount
)

export const getPrice = (state: AppState) => (
    state.pricesState.price
)

export const getError = (state: AppState) => (
    state.pricesState.error
)