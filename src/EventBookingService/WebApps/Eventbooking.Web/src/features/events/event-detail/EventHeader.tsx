import React from 'react';
import { Link } from 'react-router-dom';

const EventHeader: React.FC = () => {
  return (
    <nav className="flex mb-4" aria-label="Breadcrumb">
      <ol className="inline-flex items-center space-x-1 md:space-x-3">
        <li className="inline-flex items-center">
          <Link to="/" className="text-gray-700 hover:text-blue-600">
            Home
          </Link>
        </li>
        <li>
          <div className="flex items-center">
            <span className="mx-2 text-gray-400">/</span>
            <Link to="/events" className="text-gray-700 hover:text-blue-600">
              Events
            </Link>
          </div>
        </li>
        <li>
          <div className="flex items-center">
            <span className="mx-2 text-gray-400">/</span>
            <span className="text-gray-500">Event Details</span>
          </div>
        </li>
      </ol>
    </nav>
  );
};

export default EventHeader;