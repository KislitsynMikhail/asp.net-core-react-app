import React from "react";
import {AppState} from "../../redux/store";
import {connect} from "react-redux";
import LoginForm, {LoginFormValues} from "./LoginForm";
import {requestTokens, clearError} from "../../redux/reducers/tokens-reducer";
import { Redirect } from "react-router-dom";
import {getError, getIsAuth} from "../../redux/selectors/tokens-selectors";
import {Error} from "../../api/api";

type MapStateToProps = {
    isAuth: boolean,
    error: Error
}
const mapStateToProps = (state: AppState): MapStateToProps => ({
        isAuth: getIsAuth(state),
        error: getError(state)
    }
)

type MapDispatchToProps = {
    requestTokens: (login: string, password: string) => void
    clearError: () => void
}


const getInitLoginErrorMessage = (error: Error) => {
    let errorMessage = ''
    if (error.data === 'User' && error.title === 'Not found')
        errorMessage = 'Пользователь не найден'

    return errorMessage
}

const getInitPasswordErrorMessage = (error: Error) => {
    let errorMessage = ''
    if (error.title === 'Wrong password')
        errorMessage = 'Неверный пароль'

    return errorMessage
}

const LoginContainer: React.FC<MapStateToProps & MapDispatchToProps> = ({
                                                                            requestTokens,
                                                                            isAuth,
                                                                            error,
                                                                            clearError}) => {
    const handleSubmit = (formData: LoginFormValues) => {
        requestTokens(formData.login, formData.password)
    }

    if(isAuth) {
        return <Redirect to={"/"}/>
    }
    const initLoginError = getInitLoginErrorMessage(error)
    const initPasswordError = getInitPasswordErrorMessage(error)

    return <LoginForm
        handleSubmit={handleSubmit}
        clearError={clearError}
        initPasswordError={initPasswordError}
        initLoginError={initLoginError} />
}

export default connect(mapStateToProps, {requestTokens, clearError})(LoginContainer)
