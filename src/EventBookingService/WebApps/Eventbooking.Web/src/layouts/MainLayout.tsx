import Footer from "@/components/Footer"
import Header from "@/components/Header"
import { Toaster } from "@/components/ui/toaster"
import { Outlet } from "react-router-dom"

export const MainLayout = () => {
    return (
        <div className="min-h-screen bg-white text-gray-900">
            <Header />
            <Outlet />
            <Toaster />
            <Footer />
        </div>
    )
}