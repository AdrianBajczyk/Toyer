
import {JSONObject} from "./JsonType.ts"

type RequestOptions = {
    method: string,
    headers: { 'Content-Type' : string},
    body: string
}


export async function get<T>(url: string){
    const response = await fetch(url)

    if (!response.ok){
        throw new Error(`Faied to fetch data. ${response.status}.`);
    }

    const data = await response.json() as unknown;
    return data as T;
}

export async function post<T>(url: string, reqBody: JSONObject){

    const stringBody : string = JSON.stringify(reqBody);

    const requestOptions : RequestOptions = {
        method: 'POST',
        headers: {'Content-Type': 'application/json'},
        body: stringBody
    }

    const response = await fetch(url, requestOptions)

    if (!response.ok){
        throw new Error(`Failed to fetch data. ${response.status}.`);
    }

    const data = await response.json() as unknown;
    return data as T;

}