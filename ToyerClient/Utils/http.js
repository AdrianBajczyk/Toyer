import axios from "../src/Api/axios";

const excludedStatusCodes = [422, 401];

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
    if (error.response && !excludedStatusCodes.includes(error.response.status)) {
      throw new Response(JSON.stringify(error.response.data.error), {
        status: error.response.data.status,
        statusText: error.response.data.message,
      });
    } else if (error.request && !excludedStatusCodes.includes(error.request.status)) {
      console.log(error.request);
      throw new Response("No response. Server is offline. Try again later.", {
        status: 500,
        statusText: "Connection error.",
      });
    } else if (excludedStatusCodes.includes(error.response.status)) {
      console.log(error.response)
      return error.response.data;
    }
  }
}
