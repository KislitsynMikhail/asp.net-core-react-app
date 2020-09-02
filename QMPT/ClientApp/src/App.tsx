import React from 'react';
import {BrowserRouter} from "react-router-dom";
import AppDrawer from "./components/AppDrawer/AppDrawer";
import {connect, Provider} from "react-redux";
import {store, persistor} from "./redux/store";
import { PersistGate } from 'redux-persist/integration/react';
import HeaderContainer from "./components/Header/HeaderContainer";
import {setInitTrue} from "./api/api";
import Switcher from './components/Switcher/Switcher';
import {reExecuteWithNewTokens} from "./redux/reducers/tokens-reducer";

type Props = {
    reExecuteWithNewTokens: (method: () => void) => void
}
let reExecuteAfterRefreshTokens: (method: () => void) => void
export const reExecute = (method: () => void) => {
    reExecuteAfterRefreshTokens(method)
}

const App: React.FC<Props> = ({reExecuteWithNewTokens}) => {
    setInitTrue()
    reExecuteAfterRefreshTokens = reExecuteWithNewTokens

    return (
        <div>
            <HeaderContainer />
            <div>
                <AppDrawer />
                <Switcher />
            </div>
        </div>
    )
}

const AppContainer = connect(null, {reExecuteWithNewTokens})(App)

const QMPTApp: React.FC = () => {
    return (
        <Provider store={store}>
            <BrowserRouter>
                <PersistGate persistor={persistor}>
                    <AppContainer />
                </PersistGate>
            </BrowserRouter>
        </Provider>
    )
}

export default QMPTApp