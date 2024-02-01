import axios from "../src/Api/axios";

export async function get(urlRoute) {
  try {
    const response = await axios.get(urlRoute);
    return response.data;
  } catch (error) {
    if (error.response) {
      throw new Response(`${error.response.data.error}`, {
        status: error.response.data.status,
        statusText: error.response.data.message,
      });
    } else if (error.request) {
      throw new Response("No response. Server is offline. Try again later.", {
        status: 500,
        statusText: "Connection error.",
      });
    }
  }
}


export async function post(urlRoute, reqBody) {
  try {
    const response = await axios.post(urlRoute, JSON.stringify(reqBody), {
      headers: { "Content-Type": "application/json" },
    });
    return response.data;
  } catch (error) {
    if (error.response && error.response.status !== 422) {
      throw new Response(JSON.stringify(error.response.data.error), {
        status: error.response.data.status,
        statusText: error.response.data.message,
      });
    } else if (error.request && error.request.status !== 422) {
      console.log(error.request);
      throw new Response("No response. Server is offline. Try again later.", {
        status: 500,
        statusText: "Connection error.",
      });
    } else if (error.response.status == 422) {
      return error.response.data;
    }
  }
}
