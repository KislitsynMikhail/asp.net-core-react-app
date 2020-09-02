import {BaseThunk, InferAction} from "../store";

const initialState = {
    user: {
        id: 0,
        login: '',
        firstName: '',
        lastName: ''
    },
    error: {
        data: "",
        title: "",
        statusCode: 0
    }
}

const usersReducer = (state = initialState, action: Action): InitialState => {
    switch (action.type) {
        case "users/setUser":
            return {
                ...state,
                user: action.user
            }
        default:
            return state
    }
}

export const actions = {
    setUser: (user: User) => ({type: 'users/setUser', user} as const)
}

export default usersReducer

export type User = typeof initialState.user
export type InitialState = typeof initialState
type Action = InferAction<typeof actions>
type Thunk = BaseThunk<Action>