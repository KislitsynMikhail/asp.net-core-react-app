import {AppState} from "../store";

export const getError = (state: AppState) => {
    return state.tokensState.error
}

export const getRefreshToken = (state: AppState) => {
    return state.tokensState.tokens.refreshToken
}

export const getIsAuth = (state: AppState) => {
    return state.tokensState.isAuth
}