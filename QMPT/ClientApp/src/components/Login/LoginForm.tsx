import React, {useState} from "react";
import Container from "@material-ui/core/Container";
import {CssBaseline, makeStyles} from "@material-ui/core";
import Avatar from "@material-ui/core/Avatar";
import LockOutlinedIcon from '@material-ui/icons/LockOutlined';
import Typography from "@material-ui/core/Typography";
import Button from "@material-ui/core/Button";
import {Formik, Field, Form, FormikHelpers} from 'formik';

const useStyles = makeStyles((theme) => ({
    paper: {
        marginTop: theme.spacing(12),
        display: 'flex',
        flexDirection: 'column',
        alignItems: 'center',
    },
    avatar: {
        margin: theme.spacing(1),
        backgroundColor: theme.palette.secondary.main,
    },
    form: {
        width: '100%', // Fix IE 11 issue.
        marginTop: theme.spacing(1),
    },
    submit: {
        margin: theme.spacing(3, 0, 2),
    },
    input: {
        height: 50,
        width: "100%",
        marginTop: 24,
        fontSize: 16,
        paddingLeft: 8
    },
    span: {
        color: "red"
    }
}))

const getErrorMessage = (str: string, minLength: number, maxLength: number) => {
    let errorMessage = ''
    if (str === '')
        errorMessage = 'Обязательное поле'
    else if (str.length < minLength)
        errorMessage = `Длина должна быть больше ${minLength} символов`
    else if (str.length > maxLength)
        errorMessage = `Длина должна быть меньше ${maxLength} символов`

    return errorMessage
}

type Props = {
    handleSubmit: (formData: LoginFormValues) => void
    clearError: () => void
    initLoginError: string
    initPasswordError: string
}
const LoginForm: React.FC<Props> = ({handleSubmit, initLoginError, initPasswordError, clearError}) => {
    const classes = useStyles()

    const [submitPressed, setSubmitPressed] = useState(false)
    const [loginError, setLoginError] = useState()
    const [passwordError, setPasswordError] = useState()

    const validateLogin = (login: string) => {
        setLoginError(getErrorMessage(login,  5, 100))
    }

    const  validatePassword = (password: string) => {
        setPasswordError(getErrorMessage(password, 5, 500))
    }

    const clearLoginError = () => {
        clearError()
        setLoginError('')
    }

    const clearPasswordError = () => {
        clearError()
        setPasswordError('')
    }

    return (
        <Container component="main" maxWidth="xs">
            <CssBaseline />
            <div className={classes.paper}>
                <Avatar className={classes.avatar}>
                    <LockOutlinedIcon />
                </Avatar>
                <Typography component="h1" variant="h5">
                    Вход в систему
                </Typography>
                <Formik
                    initialValues={{
                        login: '',
                        password: ''
                    }}
                    onSubmit={(values: LoginFormValues,
                    { setSubmitting }: FormikHelpers<LoginFormValues>) => {
                        setSubmitPressed(true)
                        setLoginError('')
                        setPasswordError('')
                        validateLogin(values.login)
                        validatePassword(values.password)
                        if (loginError === '' && passwordError === '')
                            handleSubmit(values)

                        setSubmitting(false)
                    }}
                    render={({handleSubmit, values}) => (
                        <Form className={classes.form} onSubmit={handleSubmit}>
                            <div>
                                <Field
                                    className={classes.input}
                                    id={"login"}
                                    name={"login"}
                                    placeholder={"Логин *"}
                                    onFocus={clearLoginError}
                                    onBlur={() => validateLogin(values.login)}
                                />
                                {submitPressed && initLoginError && <span className={classes.span}>{initLoginError}</span>}
                                {loginError && <span className={classes.span}>{loginError}</span>}
                                <Field
                                    className={classes.input}
                                    id={"password"}
                                    name={"password"}
                                    placeholder={"Пароль *"}
                                    type={"password"}
                                    onFocus={clearPasswordError}
                                    onBlur={() => validatePassword(values.password)}
                                />
                                {submitPressed && initPasswordError && <span className={classes.span}>{initPasswordError}</span>}
                                {passwordError && <span className={classes.span}>{passwordError}</span>}
                            </div>
                            <Button
                                type="submit"
                                fullWidth
                                variant="contained"
                                color="primary"
                                className={classes.submit}
                            >
                                Войти
                            </Button>
                        </Form>
                    )}
                >
                </Formik>
            </div>
            <div>
                <div>
                    Логин: login
                </div>
                <div>
                    Пароль: password
                </div>
            </div>
        </Container>
    )
}

export default LoginForm

export type LoginFormValues = {
    login: string,
    password: string
}