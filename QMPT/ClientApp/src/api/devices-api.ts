import {AxiosResponse} from "axios";
import {Error, getAccessToken, instance} from "./api";
import {Device, Devices} from "../redux/reducers/devices-reducer";

export type Responses = Promise<AxiosResponse<Devices | Error>>
export type Response = Promise<AxiosResponse<Device | Error>>

export const devicesAPI = {
    getDevices(page: number, count: number, name: string): Responses {
        return instance.get(`devices?page=${page}&count=${count}${name ? `&name=${name}` : ''}`,
            {headers: {Authorization: getAccessToken()}})
    },
    getDevice(id: number): Response {
        return instance.get(`devices/${id}`, {headers: {Authorization: getAccessToken()}})
    },
    addDevice(device: Device): Response {
        return instance.post(`devices`, device, {headers: {Authorization: getAccessToken()}})
    },
    editDevice(device: Device): Response {
        return instance.patch(`devices/${device.id}`, device, {headers: {Authorization: getAccessToken()}})
    },
    deleteDevice(deviceId: number) {
        return instance.delete(`devices/${deviceId}`, {headers: {Authorization: getAccessToken()}})
    }
}