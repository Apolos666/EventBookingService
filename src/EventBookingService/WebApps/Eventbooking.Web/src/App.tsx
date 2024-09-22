import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Header from './layouts/Header';
import Footer from './layouts/Footer';
import Homepage from './pages/HomePage';
import ViewMoreEvents from './pages/ViewMoreEvents';
import EventDetails from './pages/EventDetails';
import CheckoutSuccessPage from './features/checkout/CheckoutSuccessPage';
import CheckoutCancelPage from './features/checkout/CheckoutCancelPage';

const App: React.FC = () => {
    return (
        <Router>
            <div className="min-h-screen bg-white text-gray-900">
                <Header />
                <Routes>
                    <Route path="/" element={<Homepage />} />
                    <Route path="/events" element={<ViewMoreEvents />} />
                    <Route path="/events/:id" element={<EventDetails />} />
                    <Route path="/checkout/success" element={<CheckoutSuccessPage />} />
                    <Route path="/checkout/canceled" element={<CheckoutCancelPage />} />
                    <Route path="*" element={<div>404 Not Found</div>} />
                </Routes>
                <Footer />
            </div>
        </Router>
    );
};

export default App;