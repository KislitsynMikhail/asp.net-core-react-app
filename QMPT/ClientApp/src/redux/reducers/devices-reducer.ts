import {AppState, BaseThunk, InferAction} from "../store";
import {AxiosResponse} from "axios";
import {Dispatch} from "react";
import {getError} from "../selectors/organizations-selectors";
import {emptyError, StatusCodes, Error} from "../../api/api";
import {reExecute} from "../../App";
import {devicesAPI} from "../../api/devices-api";
import {emptyDevice} from "../../components/Devices/DeviceForm";

const initialState = {
    device: emptyDevice as Device,
    devices: [] as Array<Device>,
    count: 0,
    error: emptyError
}

const devicesReducer = (state = initialState, action: Action): InitialState => {
    switch (action.type) {
        case "devices/setDevices":
            return {
                ...state,
                devices: action.devices
            }
        case "devices/addDevice":
            return {
                ...state,
                devices: [action.device, ...state.devices]
            }
        case "devices/editDevice":
            const deviceIndex = state.devices.findIndex(val => val.id === action.device.id)
            return {
                ...state,
                devices: [...state.devices.slice(0, deviceIndex), action.device, ...state.devices.slice(deviceIndex + 1)]
            }
        case "devices/deleteDevice":
            return {
                ...state,
                devices: state.devices.filter(device => device.id !== action.deviceId)
            }
        case "devices/setCount":
            return {
                ...state,
                count: action.count
            }
        case "devices/setError":
            return {
                ...state,
                error: action.error
            }
        default:
            return state
    }
}

export const actions = {
    setDevice: (device: Device) => ({type: 'devices/setDevice', device} as const),
    setDevices: (devices: Array<Device>) => ({type: 'devices/setDevices', devices} as const),
    addDevice: (device: Device) => ({type: 'devices/addDevice', device} as const),
    editDevice: (device: Device) => ({type: 'devices/editDevice', device} as const),
    deleteDevice: (deviceId: number) => ({type: 'devices/deleteDevice', deviceId} as const),
    setCount: (count: number) => ({type: 'devices/setCount', count} as const),
    setError: (error: Error) => ({type: 'devices/setError', error} as const),
    clearError: () => actions.setError(emptyError)
}

export const requestDevice = (id: number): Thunk => {
    return async (dispatch, getState) => {
        const requestDevice = async (id: number) => {
            try {
                const deviceResponse = await devicesAPI.getDevice(id)
                const device = deviceResponse.data as Device

                dispatch(actions.setDevice(device))
                dispatch(actions.clearError())
            } catch (error) {
                handleUnauthorizedRequest(error.response, getState(), dispatch,
                    () => requestDevice(id))
            }
        }
        await requestDevice(id)
    }
}

export const requestDevices = (page: number, count: number, name: string): Thunk => {
    return async (dispatch, getState) => {
        const requestDevices = async (page: number, count: number, name: string) => {
            try {
                const devicesResponse = await devicesAPI.getDevices(page, count, name)
                const devices = devicesResponse.data as Devices

                dispatch(actions.setDevices(devices.devices))
                dispatch(actions.setCount(devices.count))
                dispatch(actions.clearError())
            } catch (error) {
                handleUnauthorizedRequest(error.response, getState(), dispatch,
                    () => requestDevices(page, count, name))
            }
        }
        await requestDevices(page, count, name)
    }
}

export const addDevice = (device: Device): Thunk => {
    return async (dispatch, getState) => {
        const state = getState()
        const addDevice = async (device: Device) => {
            try {
                const deviceResponse = await devicesAPI.addDevice(device)
                device = deviceResponse.data as Device

                dispatch(actions.addDevice(device))
                dispatch(actions.setCount(state.devicesState.count + 1))
                dispatch(actions.clearError())
            } catch (error) {
                handleUnauthorizedRequest(error.response, state, dispatch,
                    () => addDevice(device))
            }
        }
        await addDevice(device)
    }
}

export const editDevice = (device: Device): Thunk => {
    return async (dispatch, getState) => {
        const editDevice = async (device: Device) => {
            try {
                const deviceResponse = await devicesAPI.editDevice(device)
                device = deviceResponse.data as Device

                dispatch(actions.editDevice(device))
                dispatch(actions.clearError())
            } catch (error) {
                handleUnauthorizedRequest(error.response, getState(), dispatch,
                    () => editDevice(device))
            }
        }
        await editDevice(device)
    }
}

export const deleteDevice = (id: number): Thunk => {
    return async (dispatch, getState) => {
        const state = getState()
        const deleteDevice = async (id: number) => {
            try {
                await devicesAPI.deleteDevice(id)

                dispatch(actions.deleteDevice(id))
                dispatch(actions.setCount(state.devicesState.count - 1))
                dispatch(actions.clearError())
            } catch (error) {
                handleUnauthorizedRequest(error.response, state, dispatch,
                    () => deleteDevice(id))
            }
        }
        await deleteDevice(id)
    }
}

const handleUnauthorizedRequest = (response: AxiosResponse, state: AppState, dispatch: Dispatch<Action>, method: () => void) => {
    if (response === undefined) {
        return
    }

    const error = getError(state)
    dispatch(actions.setError(response.data))
    if (response.status === StatusCodes.Unauthorized && error.statusCode !== StatusCodes.Unauthorized) {
        reExecute(method)
    }
}

export default devicesReducer

export type AdmissibleRandomErrorMax = {
    seconds: string
    value: string
}
export type Device = {
    id: number
    number: string
    measurementRange: string
    permissibleSystematicErrorMax: string
    admissibleRandomErrorsMax: Array<AdmissibleRandomErrorMax>
    magnetometerReadingsVariation: string
    gradientResistance: string
    signalAmplitude: string
    relaxationTime: string
    optimalCycle: string
}
export type Devices = {
    devices: Array<Device>
    count: number
}
export type InitialState = typeof initialState
type Action = InferAction<typeof actions>
type Thunk = BaseThunk<Action>