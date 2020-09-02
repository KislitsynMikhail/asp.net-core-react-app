import {Error, getAccessToken, instance} from "./api";
import {AxiosResponse} from "axios";
import {Price, PriceType, Prices} from "../redux/reducers/prices-reducer";

export type Responses = Promise<AxiosResponse<Prices | Error>>
export type Response = Promise<AxiosResponse<Price | Error>>

export const pricesAPI = {
    getPrices(type: PriceType, page: number, count: number, name: string): Responses {
        return instance.get(`prices?type=${type}&page=${page}&count=${count}${name ? '&name=' + name : ''}`,
            {headers: {Authorization: getAccessToken()}})
    },
    getPrice(id: number): Response {
        return instance.get(`prices/${id}`,{headers: {Authorization: getAccessToken()}})
    },
    postPrice(price: Price): Response {
        return instance.post(`prices`, price, {headers: {Authorization: getAccessToken()}})
    },
    patchPrice(price: Price): Response {
        return instance.patch(`prices/${price.id}`, price, {headers: {Authorization: getAccessToken()}})
    },
    deletePrice(priceId: number) {
        return instance.delete(`prices/${priceId}`, {headers: {Authorization: getAccessToken()}})
    }
}