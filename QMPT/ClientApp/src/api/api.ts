import axios from "axios";
import {store} from "../redux/store";

let isInit = false
export const setInitTrue = () => {
    isInit = true
}

export enum StatusCodes {
    Ok = 200,
    BadRequest = 400,
    Unauthorized = 401
}

export type Error = {
    data: string,
    title: string,
    statusCode: number
}
export const emptyError = {
    data: '',
    title: '',
    statusCode: 0
} as Error

export const getAccessToken = (): string => {
    if (isInit) {
        return store.getState().tokensState.tokens.accessToken
    }

    return ''
}

export const instance = axios.create({
    baseURL: 'https://localhost:5001/api/'
})