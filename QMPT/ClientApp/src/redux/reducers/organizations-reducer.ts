import {AppState, BaseThunk, InferAction} from "../store";
import {organizationsAPI} from "../../api/organizations-api";
import {emptyError, StatusCodes, Error} from "../../api/api";
import { AxiosResponse } from "axios";
import {Dispatch} from "react";
import {getError} from "../selectors/organizations-selectors";
import {reExecute} from "../../App";

const initialState = {
    organization: {} as Organization,
    organizations: [] as Array<Organization>,
    organizationsCount: 0,
    contactPeople: [] as Array<ContactPerson>,
    emails: [] as Array<ContactItem>,
    phoneNumbers: [] as Array<ContactItem>,
    error: emptyError
}
export type Organization = {
    id: number
    creationTime?: string
    name: string
    inn: string
    kpp: string
    ogrn: string
    okpo: string
    legalAddress: string
    email: string
    phoneNumber: string
    settlementAccount: string
    corporateAccount: string
    bik: string
    managerPosition: string
    base: string
    supervisorFIO: string
    chiefAccountant: string
    isUsn: boolean
    organizationType: OrganizationsTypes
}
export type OrganizationsTypes = "Customer" | "Provider"
export type ContactPersonWithItems = {
    id: number
    organizationId: number
    name: string
    position: string
    emails: Array<ContactItem>
    phoneNumbers: Array<ContactItem>
}
export type ContactPerson = {
    id: number
    organizationId: number
    name: string
    position: string
}
export type ContactItem = {
    id: number
    contactPersonId: number
    value: string
}

export type Organizations = {
    organizations: Array<Organization>
    count: number
}
export type ConcatItems = {
    emails: Array<ContactItem>
    phoneNumbers: Array<ContactItem>
}
export type InitialState = typeof initialState
type Action = InferAction<typeof actions>
type Thunk = BaseThunk<Action>

const organizationsReducer = (state = initialState, action: Action): InitialState => {
    switch (action.type) {
        case "organizations/setOrganizations":
            return {
                ...state,
                organizations: action.organizations,
                organizationsCount: action.organizations.length
            }
        case "organizations/addOrganization":
            return {
                ...state,
                organizations: [action.organization, ...state.organizations]
            }
        case "organizations/editOrganization":
            let organizationIndex = state.organizations.findIndex(val => val.id === action.organization.id)
            return {
                ...state,
                organizations: [
                    ...state.organizations.slice(0, organizationIndex),
                    action.organization,
                    ...state.organizations.slice(organizationIndex + 1)]
            }
        case "organizations/deleteOrganization":
            return {
                ...state,
                organizations: state.organizations.filter(val => val.id !== action.organizationId)
            }
        case "organizations/setOrganizationsCount":
            return {
                ...state,
                organizationsCount: action.count
            }
        case "organizations/setContactPeople":
            return {
                ...state,
                contactPeople: action.contactPeople
            }
        case "organizations/addContactPerson":
            return {
                ...state,
                contactPeople: [...state.contactPeople, action.contactPerson]
            }
        case "organizations/editContactPerson":
            const contactPersonIndex = state.contactPeople.findIndex(val => val.id === action.contactPerson.id)
            return {
                ...state,
                contactPeople: [
                    ...state.contactPeople.slice(0, contactPersonIndex),
                    action.contactPerson,
                    ...state.contactPeople.slice(contactPersonIndex + 1)]
            }
        case "organizations/setError":
            return {
                ...state,
                error: action.error
            }
        case "organizations/setEmails":
            return {
                ...state,
                emails: action.emails
            }
        case "organizations/addEmail":
            return {
                ...state,
                emails: [...state.emails, action.email]
            }
        case "organizations/setPhoneNumbers":
            return {
                ...state,
                phoneNumbers: action.phoneNumbers
            }
        case "organizations/deleteEmail":
            return {
                ...state,
                emails: state.emails.filter(val => val.id !== action.id)
            }
        case "organizations/editEmail":
            const emailIndex = state.emails.findIndex(val => val.id === action.email.id)
            return {
                ...state,
                emails: [...state.emails.slice(0, emailIndex), action.email, ...state.emails.slice(emailIndex + 1)]
            }
        case "organizations/deletePhoneNumber":
            return {
                ...state,
                phoneNumbers: state.phoneNumbers.filter(val => val.id !== action.id)
            }
        case "organizations/addPhoneNumber":
            return {
                ...state,
                phoneNumbers: [...state.phoneNumbers, action.phoneNumber]
            }
        case "organizations/editPhoneNumber":
            let phoneNumberIndex = state.phoneNumbers.findIndex(val => val.id === action.phoneNumber.id)
            return {
                ...state,
                phoneNumbers: [...state.phoneNumbers.slice(0, phoneNumberIndex),
                    action.phoneNumber,
                    ...state.phoneNumbers.slice(phoneNumberIndex + 1)]
            }
        case "organizations/deleteContactPerson":
            return {
                ...state,
                contactPeople: state.contactPeople.filter(val => val.id !== action.id),
                emails: state.emails.filter(val => val.contactPersonId !== action.id),
                phoneNumbers: state.phoneNumbers.filter(val => val.contactPersonId !== action.id)
            }
        default:
            return state
    }
}

export const actions = {
    setOrganization: (organization: Organization) => ({type: 'organizations/setOrganization', organization} as const),
    setOrganizations: (organizations: Array<Organization>) =>
        ({type: 'organizations/setOrganizations', organizations} as const),
    clearOrganizations: () => actions.setOrganizations([]),
    addOrganization: (organization: Organization) => ({type: 'organizations/addOrganization', organization} as const),
    editOrganization: (organization: Organization) => ({type: 'organizations/editOrganization', organization} as const),
    deleteOrganization: (organizationId: number) => ({type: 'organizations/deleteOrganization', organizationId} as const),
    setOrganizationsCount: (count: number) => ({type: 'organizations/setOrganizationsCount', count} as const),
    setContactPeople: (contactPeople: Array<ContactPerson>) =>
        ({type: 'organizations/setContactPeople', contactPeople} as const),
    editContactPerson: (contactPerson: ContactPerson) => ({type: 'organizations/editContactPerson', contactPerson} as const),
    deleteContactPerson: (id: number) => ({type: 'organizations/deleteContactPerson', id} as const),
    addContactPerson: (contactPerson: ContactPerson) => ({type: 'organizations/addContactPerson', contactPerson} as const),
    setError: (error: Error) => ({type: 'organizations/setError', error} as const),
    clearError: () => actions.setError(emptyError),
    clearContactPeople: () => actions.setContactPeople([]),
    setEmails: (emails: Array<ContactItem>) => ({type: 'organizations/setEmails', emails} as const),
    deleteEmail: (id: number) => ({type: 'organizations/deleteEmail', id} as const),
    editEmail: (email: ContactItem) => ({type: 'organizations/editEmail', email} as const),
    addEmail: (email: ContactItem) => ({type: 'organizations/addEmail', email} as const),
    setPhoneNumbers: (phoneNumbers: Array<ContactItem>) => ({type: 'organizations/setPhoneNumbers', phoneNumbers} as const),
    deletePhoneNumber: (id: number) => ({type: 'organizations/deletePhoneNumber', id} as const),
    editPhoneNumber: (phoneNumber: ContactItem) => ({type: 'organizations/editPhoneNumber', phoneNumber} as const),
    addPhoneNumber: (phoneNumber: ContactItem) => ({type: 'organizations/addPhoneNumber', phoneNumber} as const)
}

export const requestOrganization = (id: number): Thunk => {
    return async (dispatch, getState) => {
        const requestOrganization = async (id: number) => {
            try {
                const organizationResponse = await organizationsAPI.getOrganization(id)
                const organization = organizationResponse.data as Organization

                dispatch(actions.setOrganization(organization))
                dispatch(actions.clearError())
            } catch (error) {
                handleUnauthorizedRequest(error.response, getState(), dispatch,
                    () => requestOrganization(id))
            }
        }
        await requestOrganization(id)
    }
}

export const requestOrganizations = (organizationType: OrganizationsTypes, page: number, count: number, name?: string): Thunk => {
    return async (dispatch, getState) => {
        const requestOrganizations = async (organizationType: OrganizationsTypes, page: number, count: number, name?: string) => {
            try {
                const organizationsResponse = await organizationsAPI.getOrganizations(organizationType, page, count, name)
                const organizations = organizationsResponse.data as Organizations

                dispatch(actions.setOrganizations(organizations.organizations))
                dispatch(actions.setOrganizationsCount(organizations.count))
                dispatch(actions.clearError())

            } catch (error) {
                handleUnauthorizedRequest(error.response, getState(), dispatch,
                    () => requestOrganizations(organizationType, page, count, name))
            }
        }
        await requestOrganizations(organizationType, page, count, name)
    }
}

export const clearOrganizations = (): Thunk => {
    return async (dispatch) => {
        dispatch(actions.clearOrganizations())
    }
}

export const addOrganization = (organization: Organization): Thunk => {
    return async (dispatch, getState) => {
        const state = getState()
        const addOrganization = async (organization: Organization) => {
            try {
                const organizationResponse = await organizationsAPI.addOrganization(organization)
                organization = organizationResponse.data as Organization

                dispatch(actions.addOrganization(organization))
                dispatch(actions.setOrganizationsCount(state.organizationsState.organizationsCount + 1))
                dispatch(actions.clearError())
            } catch (error) {
                handleUnauthorizedRequest(error.response, state, dispatch,
                    () => addOrganization(organization))
            }
        }
        await addOrganization(organization)
    }
}

export const editOrganization = (organization: Organization): Thunk => {
    return async (dispatch, getState) => {
        const editOrganization = async (organization: Organization) => {
            try {
                const organizationResponse = await organizationsAPI.editOrganization(organization)
                organization = organizationResponse.data as Organization

                dispatch(actions.editOrganization(organization))
                dispatch(actions.clearError())
            } catch (error) {
                handleUnauthorizedRequest(error.response, getState(), dispatch,
                    () => editOrganization(organization))
            }
        }
        await editOrganization(organization)
    }
}

export const deleteOrganization = (organizationId: number): Thunk => {
    return async (dispatch, getState) => {
        const state = getState()
        const deleteOrganization = async (organizationId: number) => {
            try {
                let organization = state.organizationsState.organizations
                    .find(val => val.id === organizationId) as Organization
                await organizationsAPI.deleteOrganization(organization)

                dispatch(actions.deleteOrganization(organizationId))
                dispatch(actions.setOrganizationsCount(state.organizationsState.organizationsCount - 1))
                dispatch(actions.clearError())
            } catch (error) {
                handleUnauthorizedRequest(error.response, state, dispatch,
                    () => deleteOrganization(organizationId))
            }
        }
        await deleteOrganization(organizationId)
    }
}

export const requestContactPeople = (organizationId: number): Thunk => {
    return async (dispatch, getState) => {
        const requestContactPeople = async (organizationId: number) => {
            try {
                const contactPeopleResponse = await organizationsAPI.getContactPeople(organizationId)
                const contactPeople = contactPeopleResponse.data as Array<ContactPersonWithItems>

                const emails = contactPeople.flatMap(val => val.emails)
                dispatch(actions.setEmails(emails))

                const phoneNumbers = contactPeople.flatMap(val => val.phoneNumbers)
                dispatch(actions.setPhoneNumbers(phoneNumbers))

                dispatch(actions.setContactPeople(contactPeople))

                dispatch(actions.clearError())
            } catch (error) {
                handleUnauthorizedRequest(error.response, getState(), dispatch,
                    () => requestContactPeople(organizationId))
            }
        }
        await requestContactPeople(organizationId)
    }
}
const addEmail = async (email: ContactItem, dispatch: Dispatch<Action>, state: AppState) => {
    try {
        const emailResponse = await organizationsAPI.addEmail(email)
        email = emailResponse.data as ContactItem

        dispatch(actions.addEmail(email))
        dispatch(actions.clearError())
    } catch (error) {
        handleUnauthorizedRequest(error.response, state, dispatch,
            () => addEmail(email, dispatch, state))
    }
}
const editEmail = async (email: ContactItem, currentValue: string, dispatch: Dispatch<Action>, state: AppState) => {
    try {
        if (currentValue !== email.value) {
            const emailResponse = await organizationsAPI.editEmail(email)
            email = emailResponse.data as ContactItem

            dispatch(actions.editEmail(email))
            dispatch(actions.clearError())
        }
    } catch (error) {
        handleUnauthorizedRequest(error.response, state, dispatch,
            () => editEmail(email, currentValue, dispatch, state))
    }
}
const addPhoneNumber = async (phoneNumber: ContactItem, dispatch: Dispatch<Action>, state: AppState) => {
    try {
        const phoneNumberResponse = await organizationsAPI.addPhoneNumber(phoneNumber)
        phoneNumber = phoneNumberResponse.data as ContactItem

        dispatch(actions.addPhoneNumber(phoneNumber))
        dispatch(actions.clearError())
    } catch (error) {
        handleUnauthorizedRequest(error.response, state, dispatch,
            () => addPhoneNumber(phoneNumber, dispatch, state))
    }
}
const editPhoneNumber = async (phoneNumber: ContactItem, currentValue: string, dispatch: Dispatch<Action>, state: AppState) => {
    try {
        if (currentValue !== phoneNumber.value) {
            const phoneNumberResponse = await organizationsAPI.editPhoneNumber(phoneNumber)
            phoneNumber = phoneNumberResponse.data as ContactItem

            dispatch(actions.editPhoneNumber(phoneNumber))
            dispatch(actions.clearError())
        }
    } catch (error) {
        handleUnauthorizedRequest(error.response, state, dispatch,
            () => editPhoneNumber(phoneNumber, currentValue, dispatch, state))
    }
}
const editContactPerson = async (contactPersonWithItems: ContactPersonWithItems, contactPerson: ContactPerson, dispatch: Dispatch<Action>, state: AppState) => {
    try {
        if (contactPerson.name !== contactPersonWithItems.name || contactPerson.position !== contactPersonWithItems.position) {
            const contactPersonResponse = await organizationsAPI.editContactPerson(contactPersonWithItems as ContactPerson)
            contactPerson = contactPersonResponse.data as ContactPerson

            dispatch(actions.editContactPerson(contactPerson))
            dispatch(actions.clearError())
        }
    } catch (error) {
        handleUnauthorizedRequest(error.response, state, dispatch,
            () => editContactPerson(contactPersonWithItems, contactPerson, dispatch, state))
    }

    for (let i = 0; i < contactPersonWithItems.emails.length; i++) {
        const email = contactPersonWithItems.emails[i]
        const foundEmail = state.organizationsState.emails.find(val => val.id === email.id)
        if (foundEmail) {
            await editEmail(email, foundEmail.value, dispatch, state)
        } else {
            email.contactPersonId = contactPerson.id
            await addEmail(email, dispatch, state)
        }
    }

    for (let i = 0; i < contactPersonWithItems.phoneNumbers.length; i++) {
        const phoneNumber = contactPersonWithItems.phoneNumbers[i]
        const foundPhoneNumber = state.organizationsState.phoneNumbers.find(val => val.id === phoneNumber.id)
        if (foundPhoneNumber) {
            await editPhoneNumber(phoneNumber, foundPhoneNumber.value, dispatch, state)
        } else {
            phoneNumber.contactPersonId = contactPerson.id
            await addPhoneNumber(phoneNumber, dispatch, state)
        }
    }
}
const addContactPerson = async (contactPersonWithItems: ContactPersonWithItems, dispatch: Dispatch<Action>, state: AppState) => {
    let contactPersonId = 0
    try {
        const contactPersonResponse = await organizationsAPI.addContactPerson(contactPersonWithItems)
        const contactPerson = contactPersonResponse.data as ContactPerson
        contactPersonId = contactPerson.id

        dispatch(actions.addContactPerson(contactPerson))
        dispatch(actions.clearError())
    } catch (error) {
        handleUnauthorizedRequest(error.response, state, dispatch,
            () => addContactPerson(contactPersonWithItems, dispatch, state))
    }

    for (let i = 0; i < contactPersonWithItems.emails.length; i++) {
        const email = contactPersonWithItems.emails[i]
        email.contactPersonId = contactPersonId

        await addEmail(email, dispatch, state)
    }

    for (let i = 0; i < contactPersonWithItems.phoneNumbers.length; i++) {
        const phoneNumber = contactPersonWithItems.phoneNumbers[i]
        phoneNumber.contactPersonId = contactPersonId

        await addPhoneNumber(phoneNumber, dispatch, state)
    }
}
export const addOrEditContactPerson = (contactPersonWithItems: ContactPersonWithItems): Thunk => {
    return async (dispatch, getState) =>{
        const state = getState()
        let contactPerson = state.organizationsState.contactPeople.find(val => val.id === contactPersonWithItems.id) as ContactPerson
        if (contactPerson) {
            await editContactPerson(contactPersonWithItems, contactPerson, dispatch, state)
        } else {
            await addContactPerson(contactPersonWithItems, dispatch, state)
        }
    }
}

export const deleteContactPerson = (id: number): Thunk => {
    return async (dispatch, getState) => {
        const deleteContactPerson = async (id: number) => {
            try {
                await organizationsAPI.deleteContactPerson(id)

                dispatch(actions.deleteContactPerson(id))
                dispatch(actions.clearError())
            } catch (error) {
                handleUnauthorizedRequest(error.response, getState(), dispatch,
                    () => deleteContactPerson(id))
            }
        }
        await deleteContactPerson(id)
    }
}

export const deleteEmail = (id: number): Thunk => {
    return async (dispatch, getState) => {
        const deleteEmail = async (id: number) => {
            try {
                await organizationsAPI.deleteEmail(id)

                dispatch(actions.deleteEmail(id))
                dispatch(actions.clearError())
            } catch (error) {
                handleUnauthorizedRequest(error.response, getState(), dispatch,
                    () => deleteEmail(id))
            }
        }
        await deleteEmail(id)
    }
}

export const deletePhoneNumber = (id: number): Thunk => {
    return async (dispatch, getState) => {
        const deletePhoneNumber = async (id: number) => {
            try {
                await organizationsAPI.deletePhoneNumber(id)

                dispatch(actions.deletePhoneNumber(id))
                dispatch(actions.clearError())
            } catch (error) {
                handleUnauthorizedRequest(error.response, getState(), dispatch,
                    () => deletePhoneNumber(id))
            }
        }
        await deletePhoneNumber(id)
    }
}

export const clearContactPeople = (): Thunk => {
    return async (dispatch) => {
        dispatch(actions.clearContactPeople())
    }
}

const handleUnauthorizedRequest = (response: AxiosResponse, state: AppState, dispatch: Dispatch<Action>, method: () => void) => {
    const error = getError(state)
    if (response === undefined) {
        return
    }
    if (response.status === StatusCodes.Unauthorized && error === emptyError) {
        reExecute(() => {
            dispatch(actions.setError(response.data))
            method()
        })

    }
}

export default organizationsReducer