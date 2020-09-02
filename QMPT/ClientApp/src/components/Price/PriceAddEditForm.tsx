import React from "react";
import Button from "@material-ui/core/Button";
import {columns, PriceTableColumnId} from "./PriceTable";
import {makeStyles} from "@material-ui/core";
import { Errors } from "./Prices";
import {PriceType} from "../../redux/reducers/prices-reducer";

const useStyles = makeStyles((theme) => ({
    formControl: {
        minWidth: 120,
        width: "97%"
    },
    form: {
        width: '100%', // Fix IE 11 issue.
        display: "flex",
        flexDirection: "column",
        justifyContent: "end"
    },
    button: {
        width: '15%',
        margin: theme.spacing(0, 0, 2),
        marginLeft: "0.5%",
        minWidth: 100
    },
    input: {
        width: "100%",
        marginRight: 20,
        marginBottom: 20
    },
    inputFieldDiv: {
        height: 50,
        width: "95%",
        fontSize: 16,
        paddingLeft: 8,
        '&$focus': {
            color: '#3f51b5'
        }
    },
    inputDiv: {
        display: "flex",
        flexDirection: "column",
        flexWrap: "wrap",
        justifyContent: "space-around",
    },
    labelDiv: {
        paddingLeft: 10
    },
    errorSpan: {
        color: 'red'
    },
    div: {
        width: '47.5%',
    }
}))

const validationColumnsState = {
    name: { maxLength: 200} as ValidationRules,
    cost: { maxLength: 30 } as ValidationRules,
}

export const emptyPriceValues = {id: 0, cost: '', name: '', type: "Repair"} as PriceValues

type ValidationRules = {
    maxLength: number
}

export type PriceValues = {
    id: number
    name: string
    cost: string
    type: PriceType
}

type Props = {
    values: PriceValues
    setValues: (newValues: PriceValues) => void
    errors: Errors
    setErrors: (newErrors: Errors) => void
    isEditMode: boolean
    handleCancelButtonClick: () => void
    handleSubmit: () => void
    handleDeleteButtonClick: () => void
}

const PriceAddEditForm: React.FC<Props> = ({
                                               values, isEditMode, handleCancelButtonClick,
                                               handleSubmit, setValues, errors, setErrors,
                                               handleDeleteButtonClick}) => {
    const classes = useStyles()

    const setError = (columnId: PriceTableColumnId, error: string) => {
        const newErrors = {...errors}
        newErrors[columnId] = error
        setErrors(newErrors)
    }

    const validate = (value: string, columnId: PriceTableColumnId) => {
        const validationRule = validationColumnsState[columnId]
        if (value.length > validationRule.maxLength) {
            setError(columnId, `Длина должна быть меньше, чем ${validationRule.maxLength}`)
        }
        if (columnId === "cost") {
            const reg = /^\d+$/
            if (!reg.test(value)) {
                setError(columnId, `Стоимость может быть только числом`)
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

    const onSubmitButtonClick = () => {
        if (!isErrorExists()) {
            handleSubmit()
        }
    }

    const onChange = (event: React.ChangeEvent<HTMLInputElement>, colId: PriceTableColumnId) => {
        const value = (event.target as HTMLInputElement).value
        const newValues = {...values}
        newValues[colId] = value
        setValues(newValues)
    }

    return (
        <div className={classes.div}>
            <div className={classes.form}>
                <div className={classes.inputDiv}>
                    {columns.map((col, index) => {
                        return (
                            <div key={index} className={classes.input}>
                                <div>
                                    <div className={classes.labelDiv}>
                                        {col.label}
                                    </div>
                                    <input
                                        value={values[col.id]}
                                        className={classes.inputFieldDiv}
                                        id={col.id}
                                        name={col.id}
                                        placeholder={`${col.label}`}
                                        onChange={(event: React.ChangeEvent<HTMLInputElement>) => onChange(event, col.id)}
                                        onFocus={() => setError(col.id, '')}
                                        onBlur={() => validate(values[col.id].toString(), col.id)}
                                    />
                                </div>
                                { errors[col.id] && <span className={classes.errorSpan}>
                                    {errors[col.id]}
                                </span> }
                            </div>
                        )
                    })}
                </div>
                <div>
                    <Button
                        type="button"
                        fullWidth
                        variant="contained"
                        color="primary"
                        className={classes.button}
                        onClick={onSubmitButtonClick}
                    >
                        {isEditMode ? 'Сохранить' : 'Добавить' }
                    </Button>
                    { isEditMode && <Button
                        type="button"
                        fullWidth
                        variant="contained"
                        color="primary"
                        className={classes.button}
                        onClick={handleCancelButtonClick}
                    >
                        Отменить
                    </Button> }
                    { isEditMode && <Button
                        type="button"
                        fullWidth
                        variant="contained"
                        color="primary"
                        className={classes.button}
                        onClick={handleDeleteButtonClick}
                    >
                        Удалить
                    </Button> }
                </div>
            </div>
        </div>
    )
}

export default PriceAddEditForm