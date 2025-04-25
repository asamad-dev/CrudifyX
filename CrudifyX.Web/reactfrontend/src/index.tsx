import React from 'react';
import { createRoot } from 'react-dom/client';
import { ProductSearch } from './components/ProductSearch';

const container = document.getElementById('productSearchRoot');
if (container) {
    const root = createRoot(container);
    root.render(<ProductSearch />);
}
