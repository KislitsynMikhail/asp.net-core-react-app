import { createStore, Action, applyMiddleware, compose } from "redux";
import thunkMiddleware, {ThunkAction} from "redux-thunk";
import { persistStore } from 'redux-persist'
import rootReducer, { RootReducer } from "./reducers/root-reducer";

export type AppState = ReturnType<RootReducer>

export type InferAction<T> = T extends { [keys: string]: (...args: any[]) => infer U } ? U : never
export type BaseThunk<A extends Action = Action, R = Promise<void>> = ThunkAction<R, AppState, unknown, A>

// @ts-ignore
const composeEnhancers = window.__REDUX_DEVTOOLS_EXTENSION_COMPOSE__ || compose

export const store = createStore(rootReducer, composeEnhancers(applyMiddleware(thunkMiddleware)))
// @ts-ignore
window.__store__ = store

export const persistor = persistStore(store)

export default { store, persistor }