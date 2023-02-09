import { useQuery } from "react-query";
import { Link } from "react-router-dom";
import { API } from "../Api";
import { useAuth } from "../auth";
import './ActionBar.css'

export function ActionBar()
{
	const { isLoading, isError, data, error, refetch } = useQuery('me', API.me);
  const { signOut } = useAuth();


	return (
		<div className="actionbar">
			{
				!isLoading && !isError ? (
					<div className="actionbar-group">
						<Link to={"/"}>Tickets</Link>
						{
							(data.data.ticketPermission.create) ? (<Link to={"/create"}>Create</Link>): null
						}
						<a onClick={signOut} className="logout">Logout</a>
					</div>
				): (
					null
				)
			}
		</div>
	)
}