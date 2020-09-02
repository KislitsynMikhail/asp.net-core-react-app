import {instance, Error} from "./api";
import {Tokens} from "../redux/reducers/tokens-reducer";
import { AxiosResponse } from "axios";

export type Response = Promise<AxiosResponse<Tokens | Error>>

export const tokensAPI = {
    getTokens(login:string, password: string): Response {
        return instance.get(`tokens?login=${login}&password=${password}`)
    },
    refreshTokens(refreshToken: string): Response {
        return instance.get(`tokens/${refreshToken}`)
    },
    deleteRefreshToken(refreshToken: string) {
        instance.delete(`tokens/${refreshToken}`)
    }
}