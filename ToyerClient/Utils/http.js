

export async function get(url){
    const response = await fetch(url)

    if (!response.ok){
        const errorData = await response.json()
        throw new Response(`${errorData.error}`, { status: errorData.status, statusText: errorData.message }); 
    }

    const data = await response.json();
    return data;
}

export async function post(url, reqBody){

    const stringBody  = JSON.stringify(reqBody);
    console.log(stringBody)

    const requestOptions = {
        method: 'POST',
        headers: {'Content-Type': 'application/json'},
        body: stringBody
    }

    const response = await fetch(url, requestOptions)

    if (!response.ok){
        const errorData = await response.json()
        console.log(errorData.error)
        throw new Response(`${errorData.error}`, { status: errorData.status, statusText: errorData.message }); 
    }

    const data = await response.json();
    return data;

}