import React, {useEffect, useState} from 'react';
import {makeStyles} from '@material-ui/core/styles';
import DoneIcon from '@material-ui/icons/Done';
import TableHead from "@material-ui/core/TableHead";
import TableRow from "@material-ui/core/TableRow";
import TableCell from "@material-ui/core/TableCell";
import TableBody from "@material-ui/core/TableBody";
import Table from "@material-ui/core/Table";
import {IconButton} from "@material-ui/core";
import EditIcon from '@material-ui/icons/Edit';
import clxs from 'classnames'
import InputBase from "@material-ui/core/InputBase";
import DeleteIcon from '@material-ui/icons/Delete';
import Typography from "@material-ui/core/Typography";
import {
    ContactItem,
    ContactPerson,
    ContactPersonWithItems,
    deletePhoneNumber,
    deleteEmail,
    deleteContactPerson,
    addOrEditContactPerson,
    requestContactPeople} from "../../redux/reducers/organizations-reducer";
import { connect } from 'react-redux';
import {AppState} from "../../redux/store";
import {getContactPeople, getEmails, getPhoneNumbers} from "../../redux/selectors/organizations-selectors";

interface Column {
    id: ColumnId
    label: string;
    minWidth?: number;
    align?: 'right';
    format?: (value: number) => string;
}

type ColumnId = 'emails' | 'phoneNumbers' | 'name' | 'position'

const columns: Column[] = [
    { id: 'name', label: 'Имя', minWidth: 100 },
    { id: "position", label: 'Должность', minWidth: 100 },
    { id: "emails", label: "Email", minWidth: 100 },
    { id: "phoneNumbers", label: "Телефон", minWidth: 100 }
]
type ContactItemData = {
    id: number
    value: string
}
type ContactPersonData = {
    id: number
    name: string
    position: string
    emails: Array<ContactItemData>
    phoneNumbers: Array<ContactItemData>
}
type FormData = {
    contactPersonId: number
    disabled: boolean
    autoFocus: ColumnId | ''
    autoFocusSubRow?: number,
    errors: Error
}
type Data = {
    contactPerson: ContactPersonData
    form: FormData
}

type ChangeData = {
    columnId: ColumnId
    contactItemId?: number
    newValue: string
}

type ItemError = {
    id: number,
    error: string
}
const emptyError = {
    name: '',
    position: '',
    emails: [{id: 0, error: ''}] as Array<ItemError>,
    phoneNumbers: [{id: 0, error: ''}] as Array<ItemError>
}
type Error = typeof emptyError
const emptyForm = {
    contactPersonId: 0,
    disabled: false,
    autoFocus: '',
    errors: {...emptyError}
} as FormData
const emptyRow: Data = {
    contactPerson: { id: 0, emails: [{id: 0, value: ''}], phoneNumbers: [{id: 0, value: ''}], name: '', position: ''},
    form: {...emptyForm}
}

const validationColumnsState = {
    name: {maxLength: 100},
    position: {maxLength: 100},
    emails: {maxLength: 100},
    phoneNumbers: {maxLength: 20}
}

const useStyles = makeStyles({
    root: {
        width: '100%',
    },
    cell: {
        height: '100%',
        padding: 0,
        borderBottom: 0
    },
    inputCell: {
        width: '100%',
        height: '100%',
        minHeight: 50,
        border: 0,
    },
    disabledInputCell: {
        color: 'rgba(0, 0, 0, 0.7) !important'
    },
    errorSpan: {
        color: 'red'
    }
})

const getMaxContactPersonId = (contactPeople: Array<Data>): number => {
    let max = 1
    contactPeople.forEach(val => {
        if (val.contactPerson.id > max)
            max = val.contactPerson.id
    })

    return  max
}

const getMaxContactItemId = (data: Array<Data>): number => {
    let max = 1
    data.forEach(data => {
        data.contactPerson.emails.forEach(contactItem => {
            if (contactItem.id > max)
                max = contactItem.id
        })
        data.contactPerson.phoneNumbers.forEach(contactItem => {
            if (contactItem.id > max)
                max = contactItem.id
        })
    })

    return  max
}

class MaxItemId {
    maxId: number = 0
    constructor(getMaxId: (rows: Array<Data>) => number, rows: Array<Data>) {
        this.maxId = getMaxId(rows)
    }
    getNewId(): number {
        return ++this.maxId
    }
}

type ComponentFunctions = {
    isContactPersonFromState: (id: number) => boolean
    isEmailFromState: (id: number) => boolean
    isPhoneNumberFromState: (id: number) => boolean
    addOrEditContactPerson: (contactPersonData: Data) => void
    deleteEmail: (id: number) => void
    deletePhoneNumber: (id: number) => void
    deleteContactPerson: (id: number) => void
}
type Props = {
    initContactPeopleData: Array<Data>
} & ComponentFunctions

const ContactPersons: React.FC<Props> = ({
                                             initContactPeopleData,
                                             deleteEmail,
                                             deletePhoneNumber,
                                             deleteContactPerson,
                                             addOrEditContactPerson,
                                             isContactPersonFromState,
                                             isEmailFromState,
                                             isPhoneNumberFromState}) => {
    const classes = useStyles()

    const [contactPeopleData, setContactPeopleData] = useState(initContactPeopleData)
    useEffect(() => {
        setContactPeopleData(initContactPeopleData)
    }, [initContactPeopleData])

    const maxContactPersonId = new MaxItemId(getMaxContactPersonId, contactPeopleData)
    const maxContactItemId = new MaxItemId(getMaxContactItemId, contactPeopleData)

    const validate = (value: string, colId: ColumnId, contactPersonId: number, contactItemId?: number) => {
        const validationRule = validationColumnsState[colId]
        if (validationRule.maxLength < value.length){
            setError(colId,
                `Длина должна быть меньше, чем ${validationRule.maxLength}`,
                contactPersonId,
                contactItemId)
        }
    }

    const setError = (colId: ColumnId, errorMessage: string, contactPersonId: number, contactItemId?: number) => {
        if (contactPersonId === 0) return
        const contactPersonData = {...contactPeopleData.find(val => val.contactPerson.id === contactPersonId) as Data}
        const newErrors = {...contactPersonData.form.errors}
        if (colId === 'emails' || colId === 'phoneNumbers') {
            const itemError = newErrors[colId].find(val => val.id === contactItemId) as ItemError
            itemError.error = errorMessage
            const otherItemErrors = newErrors[colId].filter(val => val.id !== contactItemId)
            newErrors[colId] = [...otherItemErrors, itemError]
        } else {
            newErrors[colId] = errorMessage
        }
        const otherContactPeopleData = contactPeopleData.filter(val => val.contactPerson.id !== contactPersonId)
        contactPersonData.form = {...contactPersonData.form, errors: newErrors} as FormData

        setContactPeopleData([...otherContactPeopleData, contactPersonData])
    }

    const getSubRow = (contactItem: ContactItem, row: Data, column: Column) => {
        const onChangeValue = (a: any) =>
            handleChangingValue({
                newValue: a.target.value,
                columnId: column.id,
                contactItemId: contactItem.id
            }, row)

        const autoFocus = row.form.autoFocus === column.id && row.form.autoFocusSubRow === contactItem.id

        return (
            <TableRow hover role="checkbox" key={contactItem.id}>
                { getCell(contactItem.id, row, column, contactItem.value, onChangeValue, autoFocus) }
                { contactItem.value !== '' &&
                <TableCell key={contactItem + 'deleteBut'}  className={classes.cell} style={{width: 10}}>
                    <IconButton onClick={() => {onDeleteItemButtonClick(column.id as 'emails' | 'phoneNumbers', contactItem.id)}}>
                        <DeleteIcon/>
                    </IconButton>
                </TableCell> }
            </TableRow>
        )
    }

    const getCompoundCell = (row: Data, column: Column) => {
        const value = row.contactPerson[column.id] as Array<ContactItem>
        return (
            <TableCell key={column.id}  align={column.align} className={classes.cell}>
                <Table stickyHeader aria-label="sticky table">
                    <TableBody>
                        { value
                            .filter(val => val.id !== 0)
                            .sort((val1, val2) => val1.id < val2.id ? -1 : 1)
                            .map((value) => {
                                return getSubRow(value, row, column)
                            })
                        }
                        { !row.form.disabled && getSubRow(emptyRow.contactPerson[column.id][0] as ContactItem, row, column) }
                    </TableBody>
                </Table>
            </TableCell>
        )
    }

    const getCell = (id: string | number, row: Data, column: Column, value: string, onChangeValue: (a: any) => void, autoFocus: boolean) => {
        const error = column.id === 'name' || column.id === 'position'
            ? row.form.errors[column.id]
            : (row.form.errors[column.id].find(val => val.id === id) as ItemError).error

        return (
            <TableCell key={id}  className={classes.cell}>
                <InputBase
                    disabled={row.form.disabled}
                    className={clxs(classes.inputCell, {
                        [classes.disabledInputCell]: row.form.disabled
                    })}
                    value={value}
                    placeholder={column.label}
                    onChange={onChangeValue}
                    autoFocus={autoFocus}
                    onFocus={() => setError(column.id, '', row.contactPerson.id, id as number)}
                    onBlur={() => validate(value, column.id, row.contactPerson.id, id as number)}
                />
                { error && <span className={classes.errorSpan}>
                    {error}
                </span>}
            </TableCell>
        )
    }

    const getMainRow = (column: Column, row: Data) => {
        if (column.id === "emails" || column.id === "phoneNumbers")
            return getCompoundCell(row, column)

        const onChangeValue = (a: any) => {
            handleChangingValue({
                columnId: column.id,
                newValue: a.target.value}, row)
        }
        const value = row.contactPerson[column.id];
        const autoFocus = row.form.autoFocus === column.id

        return (
            <TableCell key={column.id}  align={column.align} className={classes.cell}>
                <Table stickyHeader aria-label="sticky table">
                    <TableBody>
                        <TableRow hover role="checkbox" key={column.id}>
                            {getCell(column.id, row, column, value, onChangeValue, autoFocus)}
                        </TableRow>
                    </TableBody>
                </Table>
            </TableCell>
        )
    }

    const isErrorExists = () => {
        let result = false

        contactPeopleData.forEach(contactPersonData => {
            if (contactPersonData.form.errors.name !== '' || contactPersonData.form.errors.position !== '') {
                result = true
                return
            }
            const checkErrorExists = (itemError: ItemError) => {
                if (itemError.error !== '') {
                    result = true
                    return
                }
            }
            contactPersonData.form.errors.phoneNumbers.forEach(checkErrorExists)
            contactPersonData.form.errors.emails.forEach(checkErrorExists)
        })

        return result
    }

    const onDoneOrEditButtonClick = (row: Data) => {
        if (!row.form.disabled || !isErrorExists()) {
            addOrEditContactPerson(row)
            setContactPeopleData(initContactPeopleData)
        }
        const contactPersonData = {...row}
        contactPersonData.form = {...contactPersonData.form}
        contactPersonData.form.disabled = !contactPersonData.form.disabled
        const otherContactPeopleData = contactPeopleData.filter(val => val.contactPerson.id !== row.contactPerson.id)
        setContactPeopleData([...otherContactPeopleData, contactPersonData])
    }

    const deleteItem = (columnId: 'emails' | 'phoneNumbers', itemId: number) => {
        const contactPersonData = {...contactPeopleData
                .find(val => val.contactPerson[columnId]
                    .find(val => val.id === itemId) !== undefined) as Data}
        const otherContactPeopleData = contactPeopleData
            .filter(val => val.contactPerson[columnId]
                .find(val => val.id === itemId) === undefined)

        const items = contactPersonData.contactPerson[columnId]
        contactPersonData.contactPerson[columnId] = items.filter(val => val.id !== itemId)

        setContactPeopleData([...otherContactPeopleData, contactPersonData])
    }

    const onDeleteItemButtonClick = (columnId: 'emails' | 'phoneNumbers', itemId: number) => {
        if (columnId === 'emails') {
            if (isEmailFromState(itemId)) {
                deleteEmail(itemId)
            } else {
                deleteItem('emails', itemId)
            }
        } else if (columnId === 'phoneNumbers') {
            if (isPhoneNumberFromState(itemId)) {
                deletePhoneNumber(itemId)
            } else {
                deleteItem('phoneNumbers', itemId)
            }
        }
    }

    const onDeleteRowButtonClick = (id: number) => {
        if (isContactPersonFromState(id)) {
            deleteContactPerson(id)
        } else {
            setContactPeopleData([...contactPeopleData.filter(val => val.contactPerson.id !== id)])
        }
    }

    const getEditIcon = (id: number) => {
        const input = contactPeopleData.find(val => val.contactPerson.id === id) as Data
        if (input.form.disabled)
            return <EditIcon/>
        else return <DoneIcon/>
    }

    const handleChangingValue = (changeData: ChangeData, data: Data) => {
        const row = {...data}
        const columnId = changeData.columnId
        const value = changeData.newValue
        const contactPerson = {...row.contactPerson }
        const form = {...row.form}
        form.errors = {...form.errors}
        if (contactPerson.id === 0)
            contactPerson.id = maxContactPersonId.getNewId()

        if (columnId === 'name' || columnId === 'position') {
            contactPerson[columnId] = value

        } else {
            let contactItemId = changeData.contactItemId as number
            if (contactItemId === 0) {
                contactItemId = maxContactItemId.getNewId()
                const otherErrors = form.errors[columnId].filter(val => val.id !== contactItemId)
                const newError = {id: contactItemId, error: ''} as ItemError
                form.errors[columnId] = [...otherErrors, newError]
            }
            const contItems = contactPerson[columnId].filter(val => val.id !== contactItemId)
            contactPerson[columnId] = [...contItems, {id: contactItemId, value}]
            form.autoFocusSubRow = contactItemId
        }
        form.autoFocus = columnId

        row.form = form
        row.contactPerson = contactPerson
        const otherRow = contactPeopleData.filter(val => val.contactPerson.id !== contactPerson.id)
        setContactPeopleData([...otherRow, row])
    }

    const fillByContacts = () => (
        contactPeopleData
            .sort((val1, val2) => val1.contactPerson.id > val2.contactPerson.id ? 1 : -1)
            .filter(val => val.contactPerson.id !== 0)
            .map(row => {
                return (
                    <TableRow hover role="checkbox" tabIndex={-1} key={row.contactPerson.id} >
                        <TableCell className={classes.cell} style={{width: 10}}>
                            <IconButton onClick={() => onDoneOrEditButtonClick(row)}>
                                {getEditIcon(row.contactPerson.id)}
                            </IconButton>
                        </TableCell>
                        {columns.map(column => (getMainRow(column, row)))}
                        <TableCell className={classes.cell} style={{width: 10}}>
                            <IconButton onClick={() => onDeleteRowButtonClick(row.contactPerson.id)}>
                                <DeleteIcon/>
                            </IconButton>
                        </TableCell>
                    </TableRow>
                )
            })
    )

    const fillByEmptyContact = () => (
        <TableRow hover role="checkbox" tabIndex={-1} key={0} >
            <TableCell style={{width: 10}}>
            </TableCell>
            {columns.map(column => getMainRow(column, emptyRow))}
            <TableCell style={{width: 10}}>
            </TableCell>
        </TableRow>
    )

    return (
        <div className={classes.root}>
            <Typography variant="h4" component="h4">
                Контактные лица
            </Typography>
            <Table stickyHeader aria-label="sticky table">
                <TableHead>
                    <TableRow>
                        <TableCell key={'editBut'}/>
                        {columns.map((column) => (
                            <TableCell
                                key={column.id}
                                align={column.align}
                                style={{ minWidth: column.minWidth }}
                            >
                                {column.label}
                            </TableCell>
                        ))}
                        <TableCell key={'deleteBut'}/>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {fillByContacts()}
                    {fillByEmptyContact()}
                </TableBody>
            </Table>
        </div>
    );
}

type ContainerProps = {
    organizationId: number
} & MapStateToProps & MapDispatchToProps

class ContactPersonsContainer extends React.Component<ContainerProps> {

    constructor(props: ContainerProps) {
        super(props);

        this.deleteContactPerson = this.deleteContactPerson.bind(this)
        this.deleteEmail = this.deleteEmail.bind(this)
        this.deletePhoneNumber = this.deletePhoneNumber.bind(this)
        this.addOrEditContactPerson = this.addOrEditContactPerson.bind(this)
    }

    componentDidMount() {
        this.props.requestContactPeople(this.props.organizationId)
    }

    addOrEditContactPerson(contactPersonData: Data) {
        const getItems = (items: Array<ContactItemData>) => (
            items
                .filter(val => val.id !== 0)
                .map(item => ({
                id: item.id,
                value: item.value,
                contactPersonId: contactPersonData.contactPerson.id
            }))
        )
        this.props.addOrEditContactPerson({
            ...contactPersonData.contactPerson,
            organizationId: this.props.organizationId,
            emails: getItems(contactPersonData.contactPerson.emails),
            phoneNumbers: getItems(contactPersonData.contactPerson.phoneNumbers)
        })
    }

    deleteEmail(id: number) {
        this.props.deleteEmail(id)
    }

    deletePhoneNumber(id: number) {
        this.props.deletePhoneNumber(id)
    }

    deleteContactPerson(id: number) {
        this.props.deleteContactPerson(id)
    }

    getContactPeopleData() {
        return this.props.contactPeople.map<Data>(contactPerson => {
            const emails = this.props.emails.filter(val => val.contactPersonId === contactPerson.id)
                .map<ContactItemData>(val => ({
                    id: val.id,
                    value: val.value
                }))
            const phoneNumbers = this.props.phoneNumbers.filter(val => val.contactPersonId === contactPerson.id)
                .map<ContactItemData>(val => ({
                    id: val.id,
                    value: val.value
                }))

            return {
                contactPerson: {
                    ...contactPerson,
                    emails,
                    phoneNumbers
                },
                form: {
                    ...emptyForm,
                    disabled: true,
                    errors: {
                        ...emptyError,
                        emails: emails.map(val => ({id: val.id, error: ''})).concat({id: 0, error: ''}),
                        phoneNumbers: phoneNumbers.map(val => ({id: val.id, error: ''})).concat({id: 0, error: ''})
                    }
                }
            }
        })
    }

    render() {

        const contactPeopleData = this.getContactPeopleData()

        type Id = {
            id: number
        }

        const isItemFromState = (arr: Array<Id>, id: number): boolean => {
            return arr.find(val => val.id === id) !== undefined
        }

        const isContactPersonFromState = (id: number) => {
            return isItemFromState(this.props.contactPeople, id)
        }

        const isEmailFromState = (id: number): boolean => {
            return isItemFromState(this.props.emails, id)
        }

        const isPhoneNumberFromState = (id: number): boolean => {
            return isItemFromState(this.props.phoneNumbers, id)
        }

        return <ContactPersons
            addOrEditContactPerson={this.addOrEditContactPerson}
            deleteEmail={this.deleteEmail}
            deletePhoneNumber={this.deletePhoneNumber}
            deleteContactPerson={this.deleteContactPerson}
            initContactPeopleData={contactPeopleData}
            isContactPersonFromState={isContactPersonFromState}
            isEmailFromState={isEmailFromState}
            isPhoneNumberFromState={isPhoneNumberFromState}
        />

    }
}
type MapStateToProps = {
    contactPeople: Array<ContactPerson>
    emails: Array<ContactItem>
    phoneNumbers: Array<ContactItem>
}
const mapStateToProps = (state: AppState): MapStateToProps => ({
    contactPeople: getContactPeople(state),
    emails: getEmails(state),
    phoneNumbers: getPhoneNumbers(state)
})
type MapDispatchToProps = {
    deleteEmail: (id: number) => void
    deletePhoneNumber: (id: number) => void
    deleteContactPerson: (id: number) => void
    addOrEditContactPerson: (contactPersonWithItems: ContactPersonWithItems) => void
    requestContactPeople: (organizationId: number) => void
}
const mapDispatchToProps = {
    deleteEmail,
    deletePhoneNumber,
    deleteContactPerson,
    addOrEditContactPerson,
    requestContactPeople
}
export default connect(mapStateToProps, mapDispatchToProps)(ContactPersonsContainer)