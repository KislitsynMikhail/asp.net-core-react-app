import {BaseThunk, InferAction, store} from "../store";
import {tokensAPI} from "../../api/tokens-api";
import {emptyError, Error, StatusCodes} from "../../api/api";
import {getRefreshToken} from "../selectors/tokens-selectors";
import {Dispatch} from "react";
import { AxiosResponse } from "axios";

const initialState = {
    tokens: {
        accessToken: "",
        refreshToken: ""
    },
    isAuth: false,
    error: emptyError
}

const tokensReducer = (state = initialState, action: Action): InitialState => {
    switch (action.type) {
        case 'tokens/setTokens':
            return {
                ...state,
                tokens: action.tokens
            }
        case "tokens/setError":
            return {
                ...state,
                error: action.error
            }
        case "tokens/setIsAuth":
            return {
                ...state,
                isAuth: action.isAuth
            }
        default:
            return state
    }
}

export const actions = {
    setTokens: (tokens: Tokens) => ({type: 'tokens/setTokens', tokens} as const),
    setError: (error: Error) => ({type: 'tokens/setError', error} as const),
    setIsAuth: (isAuth: boolean) => ({type: 'tokens/setIsAuth', isAuth} as const),
    clearTokens: () => actions.setTokens({accessToken: '', refreshToken: ''}),
    clearError: () => actions.setError(emptyError)
}

export const requestTokens = (login: string, password: string): Thunk => {
    return async (dispatch) => {
        try {
            const tokensResponse = await tokensAPI.getTokens(login, password)

            setTokens(dispatch, tokensResponse)
        } catch (error) {
            setError(dispatch, error.response)
        }
    }
}

export const refreshTokens = (): Thunk => {
    return async (dispatch) => {
        const refreshToken = getRefreshToken(store.getState())
        const tokensResponse = await tokensAPI.refreshTokens(refreshToken)

        if (tokensResponse.status === StatusCodes.Ok) {
            setTokens(dispatch, tokensResponse)

        } else {
            await deleteTokens(dispatch, refreshToken)
        }
    }
}

export const logout = (): Thunk => {
    return async (dispatch) => {
        const refreshToken = getRefreshToken(store.getState())
        deleteTokens(dispatch, refreshToken)

    }
}

export const clearError = (): Thunk => {
    return async (dispatch) => {
        dispatch(actions.clearError())
    }
}

export const reExecuteWithNewTokens = (method: () => void): Thunk => {
    return async (dispatch, getState) => {
        const refreshToken = getRefreshToken(getState())
        try {
            const tokensResponse = await tokensAPI.refreshTokens(refreshToken)

            setTokens(dispatch, tokensResponse)
            method()
        } catch (error) {
            deleteTokens(dispatch, refreshToken)
        }
    }
}

const deleteTokens = (dispatch: Dispatch<Action>, refreshToken: string) => {
    try {
        tokensAPI.deleteRefreshToken(refreshToken)
    } catch (error) {}

    dispatch(actions.clearTokens())
    dispatch(actions.clearError())
    dispatch(actions.setIsAuth(false))
}

const setTokens = (dispatch: Dispatch<Action>, tokensResponse: AxiosResponse<Tokens | Error>) => {
    const tokens = tokensResponse.data as Tokens
    dispatch(actions.setTokens(tokens))
    dispatch(actions.setIsAuth(true))
    dispatch(actions.clearError())
}

const setError = (dispatch: Dispatch<Action>, tokensResponse: AxiosResponse<Tokens | Error>) => {
    const error = tokensResponse.data as Error
    dispatch(actions.setError(error))
}

export default tokensReducer

export type Tokens = typeof initialState.tokens
export type InitialState = typeof initialState
type Action = InferAction<typeof actions>
type Thunk = BaseThunk<Action>