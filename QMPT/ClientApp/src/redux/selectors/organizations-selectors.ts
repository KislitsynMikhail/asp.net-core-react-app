import {AppState} from "../store";

export const getOrganizations = (state: AppState) => (
    state.organizationsState.organizations
)

export const getOrganizationsCount = (state: AppState) => (
    state.organizationsState.organizationsCount
)

export const getContactPeople = (state: AppState) => (
    state.organizationsState.contactPeople
)

export const getEmails = (state: AppState) => (
    state.organizationsState.emails
)

export const getPhoneNumbers = (state: AppState) => (
    state.organizationsState.phoneNumbers
)

export const getError = (state: AppState) => (
    state.organizationsState.error
)

