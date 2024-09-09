import { HubConnection, HubConnectionBuilder } from "@microsoft/signalr";
import { useEffect, useRef, useState } from "react";

const Notification = () => {
    const [message, setMessage] = useState<string>("");
    const [id, setId] = useState<string>("");
    const connectionRef = useRef<HubConnection>();

    useEffect(() => {
        const connection = new HubConnectionBuilder()
            .withUrl("https://localhost:5055/notifications")
            .build();

        connection.on("ClientHook", data => {
            setMessage(data.message);
            setId(data.id);
        });

        connection.start()
            .then(() => {
                connection.send("ServerHook", { id: 123, message: "Hello from client" });
            });

        connectionRef.current = connection;

        return () => {
            connection.stop();
        };
    }, []);

    const pingSelf = () => connectionRef.current?.send("SelfPing");

    const pingAll = () => connectionRef.current?.send("PingAll");

    const triggerFetch = () => fetch("https://localhost:5055/send");

    const withReturn = async () => {
        const data = await connectionRef.current?.invoke("invocation_with_return")

        console.log(data);
    };

    return (
        <>
            <div className="text-red-500">
                {id}:{message}
            </div>
            <div className="flex gap-4">
                <button className="bg-yellow-300 p-4 text-black" onClick={pingSelf}>Ping Self</button>
                <button className="bg-yellow-300 p-4 text-black" onClick={pingAll}>Ping All</button>
                <button className="bg-yellow-300 p-4 text-black" onClick={triggerFetch}>Trigger Fetch</button>
                <button className="bg-yellow-300 p-4 text-black" onClick={withReturn}>With Return</button>
            </div>
        </>
    );
};

export default Notification;