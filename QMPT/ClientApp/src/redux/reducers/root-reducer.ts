import storage from "redux-persist/lib/storage";
import {combineReducers} from "redux";
import appReducer from "./app-reducer";
import tokensReducer from "./tokens-reducer";
import persistReducer from "redux-persist/lib/persistReducer";
import usersReducer from "./users-reducer";
import organizationsReducer from "./organizations-reducer";
import pricesReducer from "./prices-reducer";
import devicesReducer from "./devices-reducer";

const persistConfig = {
    key: 'root',
    storage,
    whitelist: ['tokensState']
}

const rootReducer = combineReducers({
    appState: appReducer,
    tokensState: tokensReducer,
    usersState: usersReducer,
    organizationsState: organizationsReducer,
    pricesState: pricesReducer,
    devicesState: devicesReducer
})

export type RootReducer = typeof rootReducer

export default persistReducer(persistConfig, rootReducer)