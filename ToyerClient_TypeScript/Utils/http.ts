
import {JSONObject} from "./JsonType.ts"

type RequestOptions = {
    method: string,
    headers: { 'Content-Type' : string},
    body: string
}

type ServerErrorResponse = {
    status: number;
    message: string; 
    error: string;
}

export async function get<T>(url: string){
    const response = await fetch(url)

    if (!response.ok){
        const errorData : ServerErrorResponse = await response.json()
        console.log("debug: http.ts")
        console.log(errorData)
        throw new Response(`${errorData.error}`, { status: errorData.status, statusText: errorData.message }); 
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
        const errorData = await response.json()
        throw new Error(errorData) ;
    }

    const data = await response.json() as unknown;
    return data as T;

}