import './TicketItem.css'

export function TicketItem(props: { ticket, onresolve, canResolve }) {
	const ticketType = ["Bug", "Feature Request"];

	return (
		<div className='ticket-item'>
			<h2>{props.ticket.summery} </h2>
			<pre>{props.ticket.description}</pre>
			
			<div className='ticket-item-footer'>
				<span className='ticket-type'>{ticketType[props.ticket.ticketType]}</span>
				{ props.ticket.resolved ? 
				<span className='ticket-resolve'>Done</span> : 
				
				(props.canResolve) ? 
					(<span onClick={() => props.onresolve && props.onresolve(props.ticket)} className='button-resolve'>Resolve</span> ) : 
					null
				}
			</div>
		</div>
	)
}