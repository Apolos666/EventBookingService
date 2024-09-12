import { HubConnection, HubConnectionBuilder } from "@microsoft/signalr";
import { useEffect, useRef, useState } from "react";
import { useAuth } from "react-oidc-context";

const Notification = () => {
    const connectionRef = useRef<HubConnection>();
    const auth = useAuth();

    useEffect(() => {
        const connection = new HubConnectionBuilder()
            .withUrl("https://localhost:5055/notifications", { accessTokenFactory: () => auth.user?.access_token || "" })
            .build();

        connection.on("ReceiveNotification", (title, message) => {
            console.log(title, message);
        });  
    
        connection.start()
            .then(() => {
                console.log("Connected");
            });

        connectionRef.current = connection;

        return () => {
            connection.stop();
        };
    }, []);

    const test = () => connectionRef.current?.send("Test");

    return (
        <>            
            <div className="flex gap-4">
                <button className="bg-yellow-300 p-4 text-black" onClick={test}>Test</button>
                {/* <button className="bg-yellow-300 p-4 text-black" onClick={testEndpoint}>test endpoint</button> */}
            </div>
        </>
    );
};

export default Notification;