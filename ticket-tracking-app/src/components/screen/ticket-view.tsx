import { useMutation, useQuery } from "react-query"
import { API } from "../Api"
import { TicketItem } from "../ui";
import './ticket-view.css'

export function TicketView() {

	// would be nice put me result in top-level context
	const meResult = useQuery('me', API.me);

	const { error, data, isFetching, isSuccess, refetch } = useQuery("ticket", API.getTickets);

	const mutation = useMutation<any, any, any>(ticketid => {
		return API.resolveTicket(ticketid)
	}, {
		onSuccess: () => {
			refetch();
		},
		onError: (res) => {
			// TODO: handle http error case
		}
	})

	const resolveTicket = (ticket) => {
		mutation.mutate(ticket.id);
	}

	return (
		<div>
			<h2>Ticket list:</h2>
			<div className="ticket-list">
					{
						isSuccess && meResult.isSuccess ? (
							Array.from(data).map((ticket: any) => 
							<TicketItem 
								canResolve={meResult.data.data.ticketPermission.resolve} 
								onresolve={resolveTicket} key={ticket.id} ticket={ticket} />
							)
						): null
					}
			</div>
		</div>
	)
}