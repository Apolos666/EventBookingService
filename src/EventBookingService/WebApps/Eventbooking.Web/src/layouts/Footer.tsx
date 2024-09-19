import React from 'react';

const Footer: React.FC = () => {
  return (
    <footer className="border-t mt-12">
      <div className="container mx-auto px-4 py-6 text-center text-gray-600">
        © {new Date().getFullYear()} EventBook. All rights reserved.
      </div>
    </footer>
  );
};

export default Footer;