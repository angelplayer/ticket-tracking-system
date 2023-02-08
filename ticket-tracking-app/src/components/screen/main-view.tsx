import { useEffect } from "react";
import { useQuery, useQueryClient } from "react-query";
import { Route, Routes } from "react-router-dom";
import { API } from "../Api";
import { useAuth } from "../auth";
import { ActionBar } from "../fragement/ActionBar";
import { CreateTicketView } from "./create-ticket-view";
import { LoginView } from "./login-view";
import { TicketView } from "./ticket-view";


export function MainView() {
	const { isAuthenticated } = useAuth();

	return (
		<>
			{ isAuthenticated ? 
			(
			<main className="main-view ">
				<div className="view-container relative">
					<ActionBar></ActionBar>

					<Routes>
						<Route path="/" element={ <TicketView /> }></Route>
						<Route path="/create" element={ <CreateTicketView /> }></Route>
					</Routes>
				</div>
			</main>
			): (
				<LoginView></LoginView>
			) 
			}

			
		</>
	)
}