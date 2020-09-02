import {AppState} from "../store";

export const getDevices = (state: AppState) => (
    state.devicesState.devices
)

export const getDevice = (state: AppState) => (
    state.devicesState.device
)

export const getCount = (state: AppState) => (
    state.devicesState.count
)

export const getError = (state: AppState) => (
    state.devicesState.error
)
