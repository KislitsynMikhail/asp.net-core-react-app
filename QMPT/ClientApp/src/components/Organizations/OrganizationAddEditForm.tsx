import React, {useState} from "react";
import Button from "@material-ui/core/Button";
import {makeStyles} from "@material-ui/core";
import {Field, Form, Formik, FormikHelpers} from "formik";
import {OrganizationTableColumnId, columns} from "./OrgnaizationsTable";
import clxs from 'classnames'

const useStyles = makeStyles((theme) => ({
    form: {
        width: '100%', // Fix IE 11 issue.
        display: "flex",
        flexDirection: "column",
        justifyContent: "start"
    },
    submit: {
        width: '10%',
        margin: theme.spacing(0, 0, 2),
        marginLeft: "2%"
    },
    button: {
        width: '10%',
        margin: theme.spacing(0, 0, 2),
        marginLeft: "0.5%"
    },
    input: {
        width: "20%",
        marginRight: 20,
        marginBottom: 20
    },
    inputFieldDiv: {
        height: 50,
        width: "100%",
        fontSize: 16,
        paddingLeft: 8
    },
    checkboxInput: {
        height: 50,
        minWidth: 50,
        width: "auto",
        fontSize: 16,
        paddingLeft: 8,
    },
    inputDiv: {
        display: "flex",
        flexWrap: "wrap",
        justifyContent: "space-around",
    },
    labelDiv: {
      paddingLeft: 10
    },
    errorSpan: {
        color: 'red'
    }
}))

export const constInitialValues = {
    id: 0,
    name: '',
    inn: '',
    kpp: '',
    ogrn: '',
    okpo: '',
    legalAddress: '',
    email: '',
    phoneNumber: '',
    settlementAccount: '',
    corporateAccount: '',
    bik: '',
    managerPosition: '',
    base: '',
    supervisorFIO: '',
    chiefAccountant: '',
    isUsn: false
}

const emptyError = {
    name: '',
    inn: '',
    kpp: '',
    ogrn: '',
    okpo: '',
    legalAddress: '',
    email: '',
    phoneNumber: '',
    settlementAccount: '',
    corporateAccount: '',
    bik: '',
    managerPosition: '',
    base: '',
    supervisorFIO: '',
    chiefAccountant: '',
    isUsn: ''
}

export type AddFormData = typeof constInitialValues

type Props = {
    onEditOrAddButtonClick: (organizationData: AddFormData) => void,
    onCancelButtonClick: () => void
    initialValues?: AddFormData
    buttonNameOnEditOrAddingMode: string,
    isEditOrAddingMode: boolean
}

const validationColumnsState = {
    name: { maxLength: 200} as ValidationRules,
    inn: { maxLength: 12} as ValidationRules,
    kpp: { maxLength: 9} as ValidationRules,
    ogrn: { maxLength: 13} as ValidationRules,
    okpo: { maxLength: 10} as ValidationRules,
    legalAddress: { maxLength: 200} as ValidationRules,
    email: { maxLength: 100} as ValidationRules,
    phoneNumber: { maxLength: 20} as ValidationRules,
    settlementAccount: { maxLength: 100} as ValidationRules,
    corporateAccount: { maxLength: 100} as ValidationRules,
    bik: { maxLength: 9} as ValidationRules,
    managerPosition: { maxLength: 100} as ValidationRules,
    base: { maxLength: 100} as ValidationRules,
    supervisorFIO: { maxLength: 100} as ValidationRules,
    chiefAccountant: { maxLength: 100} as ValidationRules,
    isUsn: { maxLength: 10 } as ValidationRules
}

type ValidationRules = {
    maxLength: number
}

const OrganizationAddEditForm: React.FC<Props> = ({
                                      onCancelButtonClick,
                                      onEditOrAddButtonClick,
                                      buttonNameOnEditOrAddingMode,
                                      isEditOrAddingMode,
                                      initialValues = constInitialValues}) => {
    const classes = useStyles()
    const [errors, setErrors] = useState(emptyError)

    const validate = (value: string, colId: OrganizationTableColumnId) => {
        const validationRule = validationColumnsState[colId]
        if (validationRule.maxLength < value.length){
            setError(colId, `Длина должна быть меньше, чем ${validationRule.maxLength}`)
        }
        if (colId === "email") {
            const re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
            if (!re.test(value)) {
                setError(colId, 'Email введен некорректно')
            }
        }
    }

    const isErrorExists = () => {
        columns.forEach(val => {
            if (errors[val.id] !== '')
                return true
        })

        return false
    }

    const setError = (colId: OrganizationTableColumnId, errorMessage: string) => {
        const newErrors = {...errors}
        newErrors[colId] = errorMessage
        setErrors(newErrors)
    }

    return (
        <Formik
            initialValues={initialValues}
            onSubmit={(values: AddFormData,
                       { setSubmitting }: FormikHelpers<AddFormData>) => {
                if (!isErrorExists()) {
                    onEditOrAddButtonClick(values)
                }

                setSubmitting(false)
            }}
            render={({handleSubmit, values, ...props}) => {
                const handleCancelButtonClick = () => {
                    props.setValues(initialValues)
                    onCancelButtonClick()
                }
                return (
                <Form className={classes.form} onSubmit={handleSubmit}>
                    <div className={classes.inputDiv}>
                        {columns.map((col, index) => {
                            const type = col.id === 'isUsn' ? 'checkbox' : 'input'

                            return (
                                <div key={index} className={classes.input}>
                                    <div className={classes.labelDiv}>
                                        {col.label}
                                    </div>
                                    <Field
                                        disabled={!isEditOrAddingMode}
                                        className={clxs(classes.inputFieldDiv, {
                                            [classes.checkboxInput]: type === 'checkbox'
                                        })}
                                        id={col.id}
                                        name={col.id}
                                        placeholder={`${col.label}`}
                                        type={type}
                                        onFocus={() => setError(col.id, '')}
                                        onBlur={() => validate(values[col.id].toString(), col.id)}
                                    />
                                    { errors[col.id] && <span className={classes.errorSpan}>
                                                {errors[col.id]}
                                            </span> }
                                </div>
                            )
                        })}
                    </div>
                    <div>
                        <Button
                            type="submit"
                            fullWidth
                            variant="contained"
                            color="primary"
                            className={classes.submit}
                        >
                            {isEditOrAddingMode ? buttonNameOnEditOrAddingMode : 'Изменить'}
                        </Button>
                        <Button
                            type="button"
                            fullWidth
                            variant="contained"
                            color="primary"
                            className={classes.button}
                            onClick={handleCancelButtonClick}
                        >
                            {isEditOrAddingMode ? 'Отмена' : 'Удалить'}
                        </Button>
                    </div>
                </Form>
            )}}
        />
    )
}

export default OrganizationAddEditForm