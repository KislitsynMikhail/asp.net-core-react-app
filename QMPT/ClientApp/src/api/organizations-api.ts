import {AxiosResponse} from "axios";
import {Error, getAccessToken, instance} from "./api";
import {
    Organization,
    OrganizationsTypes,
    Organizations,
    ContactPersonWithItems, ContactPerson, ContactItem
} from "../redux/reducers/organizations-reducer";

export type Response = Promise<AxiosResponse<Organization | Error>>
export type Responses = Promise<AxiosResponse<Organizations | Error>>
export type ContactPeopleResponse = Promise<AxiosResponse<Array<ContactPersonWithItems> | Error>>
export type ContactPersonResponse = Promise<AxiosResponse<ContactPerson | Error>>
export type ContactItemResponse = Promise<AxiosResponse<ContactItem | Error>>

export const organizationsAPI = {
    getOrganizations(organizationType: OrganizationsTypes, page: number, count: number, name?: string): Responses {
        const queryStr = `Organizations/${organizationType}s?page=${page}&count=${count}${name ? '&name=' + name : ''}`
        return instance.get(queryStr, {headers: {Authorization: getAccessToken()}})
    },
    getOrganization(id: number): Response {
        return instance.get(`organizations/${id}`, {headers: {Authorization: getAccessToken()}})
    },
    addOrganization(organization: Organization): Response {
        return instance.post(`organizations/${organization.organizationType}s`, organization,
            {headers: {Authorization: getAccessToken()}})
    },
    editOrganization(organization: Organization): Response {
        return instance.patch(`organizations/${organization.organizationType}s/${organization.id}`, organization,
            {headers: {Authorization: getAccessToken()}})
    },
    deleteOrganization(organization: Organization) {
        return instance.delete(`organizations/${organization.organizationType}s/${organization.id}`,
            {headers: {Authorization: getAccessToken()}})
    },
    getContactPeople(organizationId: number) : ContactPeopleResponse {
        return instance.get(`contactPersons/${organizationId}`, {headers: {Authorization: getAccessToken()}})
    },
    addContactPerson(contactPerson: ContactPerson): ContactPersonResponse {
          return instance.post(`contactPersons`, contactPerson, {headers: {Authorization: getAccessToken()}})
    },
    editContactPerson(contactPerson: ContactPerson): ContactPersonResponse {
        return instance.patch(`contactPersons/${contactPerson.id}`, contactPerson,
            {headers: {Authorization: getAccessToken() }})
    },
    deleteContactPerson(id: number) {
        return instance.delete(`contactPersons/${id}`, {headers: {Authorization: getAccessToken() }})
    },
    editEmail(email: ContactItem): ContactItemResponse {
        return instance.patch(`contactPersonEmails/${email.id}`, email,
            {headers: {Authorization: getAccessToken()}})
    },
    addEmail(email: ContactItem): ContactItemResponse {
        return instance.post(`contactPersonEmails`, email, {headers: {Authorization: getAccessToken()}})
    },
    deleteEmail(id: number) {
        return instance.delete(`contactPersonEmails/${id}`, {headers: {Authorization: getAccessToken()}})
    },
    deletePhoneNumber(id: number) {
        return instance.delete(`contactPersonPhoneNumbers/${id}`,
            {headers: {Authorization: getAccessToken()}})
    },
    editPhoneNumber(phoneNumber: ContactItem): ContactItemResponse {
        return instance.patch(`contactPersonPhoneNumbers/${phoneNumber.id}`, phoneNumber,
            {headers: {Authorization: getAccessToken()}})
    },
    addPhoneNumber(phoneNumber: ContactItem): ContactItemResponse {
        return instance.post(`contactPersonPhoneNumbers`, phoneNumber,
            {headers: {Authorization: getAccessToken()}})
    }
}