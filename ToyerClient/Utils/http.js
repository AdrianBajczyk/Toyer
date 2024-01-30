export async function get(url) {
  try {
    const response = await fetch(url);
    console.log(response);

    if (!response.ok) {
      const errorData = await response.json();
      throw new Response(`${errorData.error}`, {
        status: errorData.status,
        statusText: errorData.message,
      });
    }
  } catch (error) {
    throw new Response(`Server is not working. Try again later.`, {
      status: 500,
      statusText: error.message,
    });
  }

  const data = await response.json();
  return data;
}

export async function post(url, reqBody) {
  const stringBody = JSON.stringify(reqBody);
  console.log(stringBody);

  const requestOptions = {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: stringBody,
  };

  try {
    const response = await fetch(url, requestOptions);

    if (!response.ok) {
      const errorData = await response.json();
      console.log(errorData.error);
      throw new Response(`${errorData.error}`, {
        status: errorData.status,
        statusText: errorData.message,
      });
    }
  } catch (error) {
    throw new Response(`Server is not working. Try again later.`, {
      status: 500,
      statusText: error.message,
    });
  }

  const data = await response.json();
  return data;
}
