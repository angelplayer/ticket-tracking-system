import { getUserToken } from "../auth/localstorage";

const BACKEND_URL = 'http://localhost:5277';

const headers = {
	"Content-Type": "Application/json"
}

const sendAccessToken = () => {
	let userToken = getUserToken();
	if(userToken.access_token) {
		return { "Authorization": `Bearer ${userToken.access_token}` }
	}

	return undefined
}

export const API = {

	login: async (credential) => {

		const response = await fetch(`${BACKEND_URL}/api/Auth/login`,  {
			method: 'POST',
			headers,
			body: JSON.stringify(credential)
		});

		return await response.json();
	},

	me: async () => {
		const response = await fetch(`${BACKEND_URL}/api/Auth/me`, {
			method: 'GET',
			headers: { ...headers, ...sendAccessToken() }
		})

		return await response.json();
	},

	getTickets: async () => {
		const response = await fetch(`${BACKEND_URL}/api/Tickets`, {
			method: 'GET',
			headers: { ...headers, ...sendAccessToken() }
		})
		return await response.json();
	},

	createTicket: async (ticket) => {

		const response = await fetch(`${BACKEND_URL}/api/Tickets`,  {
			method: 'POST',
			headers: { ...headers, ...sendAccessToken() },
			body: JSON.stringify(ticket)
		});

		return await response.json();
	},

	resolveTicket: async (ticketid) => {
		const response = await fetch(`${BACKEND_URL}/api/Tickets/${ticketid}/resolve`,  {
			method: 'PATCH',
			headers: { ...headers, ...sendAccessToken() },
		});

		return await response.json();
	}

}