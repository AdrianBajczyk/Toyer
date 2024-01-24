export async function get<T>(url: string){
    const response = await fetch(url)

    if (!response.ok){
        throw new Error(`Faied to fetch data. ${response.status} ${response.statusText}.`);
    }

    const data = await response.json() as unknown;
    return data as T;
}