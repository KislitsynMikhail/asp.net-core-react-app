import {InferAction} from "../store";

const initialState = {
    isInit: false
}
export type InitialState = typeof initialState
type Action = InferAction<typeof actions>

const appReducer = (state = initialState, action: Action): InitialState => {
    switch (action.type) {
        case 'app/successInitialization':
            return {
                ...state,
                isInit: true
            }
        default:
            return state
    }
}

export const actions = {
    successInitialization: () => ({type: 'app/successInitialization'} as const),
}

export default appReducer