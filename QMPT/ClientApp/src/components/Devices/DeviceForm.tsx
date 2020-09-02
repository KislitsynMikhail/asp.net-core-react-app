import React, {useEffect, useState} from "react";
import Paper from "@material-ui/core/Paper";
import TableContainer from "@material-ui/core/TableContainer";
import Table from "@material-ui/core/Table";
import TableRow from "@material-ui/core/TableRow";
import TableCell from "@material-ui/core/TableCell";
import TableBody from "@material-ui/core/TableBody";
import {Column, ColumnId, columns} from "./DevicesTable";
import {makeStyles, Button} from "@material-ui/core";
import InputBase from "@material-ui/core/InputBase";
import clxs from "classnames"
import {AdmissibleRandomErrorMax, Device} from "../../redux/reducers/devices-reducer";
import TableHead from "@material-ui/core/TableHead";

const useStyles = makeStyles( theme => ({
    root: {
        width: '100%',
        display: "flex",
        justifyContent: "center",
        flexDirection: "column"
    },
    container: {
        maxHeight: '100%',
    },
    cell: {
        height: '100%',
        padding: 0,
        borderBottom: 0
    },
    div: {
        width: '95%',
        marginLeft: '2.5%',
        display: "flex",
        flexDirection: "column"
    },
    inputCell: {
        width: '100%',
        height: '100%',
        border: 0,
        padding: "10px 0 10px"
    },
    label: {
        paddingLeft: 10
    },
    row: {
        cursor: "pointer",
        height: '100%'
    },
    submit: {
        marginLeft: "2%"
    },
    labelCell: {
        maxWidth: 300
    },
    button: {
        width: '10%',
        margin: theme.spacing(1, 0, 2),
        marginLeft: "0.5%"
    },
    buttonContainer: {
        display: "flex"
    },
    errorSpan: {
        marginLeft: 10,
        color: 'red'
    }
}))

export const emptyDevice = {
    id: 0,
    number: '',
    measurementRange: '',
    permissibleSystematicErrorMax: '',
    admissibleRandomErrorsMax: [{
        seconds: '0.5',
        value: ''
    }, {
        seconds: '1',
        value: ''
    }, {
        seconds: '3',
        value: ''
    }, {
        seconds: '',
        value: ''
    }] as Array<AdmissibleRandomErrorMax>,
    magnetometerReadingsVariation: '',
    gradientResistance: '',
    signalAmplitude: '',
    relaxationTime: '',
    optimalCycle: ''
} as Device

type AdmissibleRandomError = {
    index: number
    seconds: string
    value: string
}
const emptyErrors = {
    number: '',
    measurementRange: '',
    permissibleSystematicErrorMax: '',
    admissibleRandomErrorsMax: [{
        index: 0,
        seconds: '',
        value: ''
    }, {
        index: 1,
        seconds: '',
        value: ''
    }, {
        index: 2,
        seconds: '',
        value: ''
    }, {
        index: 3,
        seconds: '',
        value: ''
    }] as Array<AdmissibleRandomError>,
    magnetometerReadingsVariation: '',
    gradientResistance: '',
    signalAmplitude: '',
    relaxationTime: '',
    optimalCycle: ''
}
type Error = typeof emptyErrors

const validationColumnsState = {
    number: { maxLength: 50 } as ValidationRules,
    measurementRange: { maxLength: 50 } as ValidationRules,
    permissibleSystematicErrorMax: { maxLength: 50 } as ValidationRules,
    admissibleRandomErrorsMax: { maxLength: 50 } as ValidationRules,
    magnetometerReadingsVariation: { maxLength: 50 } as ValidationRules,
    gradientResistance: { maxLength: 50 } as ValidationRules,
    signalAmplitude: { maxLength: 50 } as ValidationRules,
    relaxationTime: { maxLength: 50 } as ValidationRules,
    optimalCycle: { maxLength: 50 } as ValidationRules
}

type ValidationRules = {
    maxLength: number
}

type Props = ContainerProps & {initErrors: Error}

const DeviceForm: React.FC<Props> = ({
                                         initDevice, submitButtonLabel, secondButtonLabel,
                                         onSubmitButtonClick, onSecondButtonClick, initErrors,
                                         onDeleteButtonClick}) => {
    const classes = useStyles()

    const [errors, setErrors] = useState(initErrors)
    useEffect(() => {
        setErrors(initErrors)
    }, [initErrors])
    const [data, setData] = useState(initDevice)
    useEffect(() => {
        setData(initDevice)
    }, [initDevice])

    const setError = (columnId: ColumnId, error: string, key?: 'seconds' | 'value', index?: number) => {
        const newErrors = {...errors}
        if (columnId === "admissibleRandomErrorsMax") {
            const errorValue = errors.admissibleRandomErrorsMax
                .find(val => val.index === index) as AdmissibleRandomError
            errorValue[key as 'seconds' | 'value'] = error
            newErrors.admissibleRandomErrorsMax = [
                ...newErrors.admissibleRandomErrorsMax.filter(val => val.index !== index),
                errorValue]
        } else {
            newErrors[columnId] = error
        }
        setErrors(newErrors)
    }

    const validate = (columnId: ColumnId, value: string, key?: 'seconds' | 'value', index?: number) => {
        const validationRule = validationColumnsState[columnId]
        if (value.length > validationRule.maxLength) {
            setError(columnId, `Длина должна быть меньше, чем ${validationRule.maxLength}`, key, index)
        }
    }

    const onChange = (event: React.ChangeEvent<HTMLInputElement>, column: Column) => {
        const value = (event.target as HTMLInputElement).value
        const newData = {...data}
        // @ts-ignore
        newData[column.id] = value
        setData(newData)
    }

    const onChangeArr = (event: React.ChangeEvent<HTMLInputElement>, ind: number, key: 'seconds' | 'value') => {
        const value = (event.target as HTMLInputElement).value
        const arr = data.admissibleRandomErrorsMax
        let newArr = [...arr]
        if (value === '' && key === 'seconds') {
            newArr = newArr.filter((val, index) => index !== ind)
            const newErrors = {...errors}
            newErrors.admissibleRandomErrorsMax = newErrors.admissibleRandomErrorsMax.filter(val => val.index !== ind)
            setErrors(newErrors)
        } else {
            const newValue = {...arr[ind]}
            newValue[key] = value
            newArr = [...arr.slice(0, ind), newValue, ...arr.slice((ind + 1))]
            if (arr.length - 1 === ind) {
                newArr = [...newArr, {seconds: '', value: ''}]
                const errorValue = {
                    index: arr.length,
                    seconds: '',
                    value: ''
                } as AdmissibleRandomError
                const newErrors = {...errors}
                newErrors.admissibleRandomErrorsMax = [...newErrors.admissibleRandomErrorsMax, errorValue]
                setErrors((newErrors))
            }
        }
        const newData = {...data, admissibleRandomErrorsMax: newArr}
        setData(newData)
    }

    const isErrorExists = () => {
        let result = false

        columns.forEach(column => {
            if (column.id === "admissibleRandomErrorsMax") {
                errors.admissibleRandomErrorsMax.forEach(val => {
                    if (val.seconds !== '' || val.value !== '') {
                        result = true
                        return
                    }
                })
            } else if (errors[column.id] !== '') {
                result = true
                return
            }
        })

        return result
    }

    const handleSubmitButtonClick = () => {
        if (!isErrorExists()) {
            const arr = data.admissibleRandomErrorsMax
            const newData = {...data, admissibleRandomErrorsMax: arr.slice(0, arr.length - 1)}

            onSubmitButtonClick(newData)
        }
    }

    const handleCancelButtonClick = () => {
        setData(initDevice)
        onSecondButtonClick()
    }

    const getCell = (column: Column) => {

        const getCell = (value: string, placeholder: string, onChange: (event: React.ChangeEvent<HTMLInputElement>) => void,
            key?: 'seconds' | 'value', index?: number) => {
            let error = ''
            if (column.id === "admissibleRandomErrorsMax") {
                const admissibleError = errors.admissibleRandomErrorsMax.find(val => val.index === index)
                if (admissibleError) {
                    error = admissibleError[key as 'seconds' | 'value']
                }
            } else {
                error = errors[column.id]
            }
            return (
                <TableCell className={classes.cell}>
                    <InputBase
                        className={clxs(classes.inputCell, classes.label)}
                        value={value}
                        placeholder={placeholder}
                        onChange={onChange}
                        onFocus={() => setError(column.id, '', key, index)}
                        onBlur={() => validate(column.id, value, key, index)}
                    />
                    {error && <span className={classes.errorSpan}>{error}</span>}
                </TableCell>
            )
        }

        if (column.id === 'admissibleRandomErrorsMax') {
            return (
                <TableCell className={classes.cell} key={column.id + 'key-value'} align={column.align}>
                    <Table stickyHeader aria-label="sticky table">
                        <TableHead>
                            <TableRow>
                                <TableCell>
                                    Секунды
                                </TableCell>
                                <TableCell>
                                    Значение
                                </TableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {data[column.id].map((val, index) => (
                                <TableRow role="checkbox" key={index} className={classes.row}>
                                    {getCell(val.seconds, 'Секунды',
                                        (event: React.ChangeEvent<HTMLInputElement>) =>
                                        onChangeArr(event, index, 'seconds'), 'seconds', index)}
                                    {getCell(val.value, 'Значение',
                                        (event: React.ChangeEvent<HTMLInputElement>) =>
                                        onChangeArr(event, index, 'value'), 'value', index)}
                                </TableRow>
                                ))}
                        </TableBody>
                    </Table>
                </TableCell>
            )
        }

        return getCell(data[column.id], column.label,
            (event: React.ChangeEvent<HTMLInputElement>) => onChange(event, column))
    }

    return (
        <div className={classes.div}>
            <Paper className={classes.root}>
                <TableContainer className={classes.container}>
                    <Table stickyHeader aria-label="sticky table">
                        <TableBody>
                            {columns.map((column, index) => {
                                return (
                                    <TableRow
                                        hover
                                        role="checkbox"
                                        tabIndex={-1}
                                        key={column.id}
                                        className={classes.row}
                                    >
                                        <TableCell className={clxs(classes.cell, classes.labelCell)} key={column.id} align={column.align}>
                                            <div className={classes.label}>
                                                {column.label}
                                            </div>
                                        </TableCell>
                                        <TableCell className={classes.cell} key={column.id + 'value'} align={column.align}>
                                            <Table stickyHeader aria-label="sticky table">
                                                <TableBody>
                                                    <TableRow hover role="checkbox" key={index} className={classes.row}>
                                                        {getCell(column)}
                                                    </TableRow>
                                                </TableBody>
                                            </Table>
                                        </TableCell>
                                    </TableRow>
                                );
                            })}
                        </TableBody>
                    </Table>
                </TableContainer>
            </Paper>
            <div className={classes.buttonContainer}>
                <Button
                    className={clxs(classes.submit, classes.button)}
                    type="button"
                    fullWidth
                    variant="contained"
                    color="primary"
                    onClick={handleSubmitButtonClick}
                >
                    {submitButtonLabel}
                </Button>
                <Button
                    className={clxs(classes.button)}
                    type="button"
                    fullWidth
                    variant="contained"
                    color="primary"
                    onClick={handleCancelButtonClick}
                >
                    {secondButtonLabel}
                </Button>
                {onDeleteButtonClick && <Button
                    className={clxs(classes.button)}
                    type="button"
                    fullWidth
                    variant="contained"
                    color="primary"
                    onClick={onDeleteButtonClick}
                    >
                    Удалить
                </Button>}
            </div>
        </div>
    )
}

type ContainerProps = {
    initDevice: Device
    submitButtonLabel: string
    secondButtonLabel: string
    onDeleteButtonClick?: () => void
    onSubmitButtonClick: (device: Device) => void
    onSecondButtonClick: () => void
}

const DeviceFormContainer: React.FC<ContainerProps> = (props) => {

    const initDevice = {
        ...props.initDevice,
        admissibleRandomErrorsMax: [...props.initDevice.admissibleRandomErrorsMax]}
    const initErrors = {
        ...emptyErrors,
        admissibleRandomErrorsMax: [...emptyErrors.admissibleRandomErrorsMax]}

    if (props.initDevice !== emptyDevice) {
        initDevice.admissibleRandomErrorsMax = [...initDevice.admissibleRandomErrorsMax, {seconds: '', value: ''}]
        initErrors.admissibleRandomErrorsMax = initDevice.admissibleRandomErrorsMax
            .map((val, ind) => ({
                index: ind,
                seconds: '',
                value: ''
            }))
    }

    return (
        <DeviceForm
            initDevice={initDevice}
            initErrors={initErrors}
            secondButtonLabel={props.secondButtonLabel}
            submitButtonLabel={props.submitButtonLabel}
            onSecondButtonClick={props.onSecondButtonClick}
            onSubmitButtonClick={props.onSubmitButtonClick}
            onDeleteButtonClick={props.onDeleteButtonClick}
        />
    )
}

export default DeviceFormContainer