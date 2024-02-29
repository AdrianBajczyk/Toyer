import axios from "../src/Api/axios";

const excludedStatusCodes = [422, 401];

export async function get(urlRoute, signal) {
  try {
    console.log(urlRoute)
    const response = await axios.get(urlRoute, {} ,{ signal: signal } );
    return response.data;
  } catch (error) {
    if (error.response) {
      console.log("errorHttp" + error);
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

export async function post(
  urlRoute,
  reqBody,
  redirectUrl = "",
  withCredentials = false
) {
  try {
    const response = await axios.post(urlRoute, JSON.stringify(reqBody), {
      headers: {
        "Content-Type": "application/json",
        "Redirect-Url": redirectUrl,
      },
      withCredentials: withCredentials,
    });
    return response.data;
  } catch (error) {
    if (
      error.response &&
      !excludedStatusCodes.includes(error.response.status)
    ) {
      throw new Response(JSON.stringify(error.response.data.error), {
        status: error.response.data.status,
        statusText: error.response.data.message,
      });
    } else if (
      error.request &&
      !excludedStatusCodes.includes(error.request.status)
    ) {
      throw new Response("No response. Server is offline. Try again later.", {
        status: 500,
        statusText: "Connection error.",
      });
    } else if (excludedStatusCodes.includes(error.response.status)) {
      return error.response.data;
    }
  }
}
