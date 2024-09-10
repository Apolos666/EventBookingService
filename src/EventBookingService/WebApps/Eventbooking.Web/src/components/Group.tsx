import { HubConnection, HubConnectionBuilder } from "@microsoft/signalr";
import { useEffect, useRef, useState } from "react";

const Group = () => {
    const [message, setMessage] = useState<string>("");
    const [id, setId] = useState<string>("");
    const connectionRef = useRef<HubConnection>();

    useEffect(() => {
        const connection = new HubConnectionBuilder()
            .withUrl("https://localhost:5055/groups")
            .build();

        connection.on("group_message", data => {
            console.log(data);
            setMessage(data.message);
            setId(data.id);
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

    const join = () => connectionRef.current?.send("Join");

    const leave = () => connectionRef.current?.send("Leave");

    function sendMessage() {
        return connectionRef.current?.send("Message");
    }
    
    return (
        <>
            <div className="text-red-500">
                {id}:{message}
            </div>
            <div className="flex gap-4">
                <button className="bg-yellow-300 p-4 text-black" onClick={join}>Join</button>
                <button className="bg-yellow-300 p-4 text-black" onClick={leave}>Leave</button>
                <button className="bg-yellow-300 p-4 text-black" onClick={() => sendMessage()}>Message</button>            
            </div>
        </>
    );
};

export default Group;