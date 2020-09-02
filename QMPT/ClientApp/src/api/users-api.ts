import {AxiosResponse} from "axios";
import {User} from "../redux/reducers/users-reducer";
import {Error, instance} from "./api";

export type Response = Promise<AxiosResponse<User | Error>>

export const usersAPI = {
    getUserById(id: number): Response {
        return instance.get(`users/${id}`)
    },
    getUser(): Response {
        return instance.get('users')
    }
}