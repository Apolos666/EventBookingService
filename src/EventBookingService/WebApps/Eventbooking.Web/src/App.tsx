import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Header from './layouts/Header';
import Footer from './layouts/Footer';
import Homepage from './pages/HomePage';
import ViewMoreEvents from './pages/ViewMoreEvents';

const App: React.FC = () => {
    return (
        <Router>
            <div className="min-h-screen bg-white text-gray-900">
                <Header />
                <Routes>
                    <Route path="/" element={<Homepage />} />
                    <Route path="/events" element={<ViewMoreEvents />} />
                </Routes>
                <Footer />
            </div>
        </Router>
    );
};

export default App;