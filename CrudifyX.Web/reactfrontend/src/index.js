import { jsx as _jsx } from "react/jsx-runtime";
import { createRoot } from 'react-dom/client';
import { ProductSearch } from './components/ProductSearch';
var container = document.getElementById('productSearchRoot');
if (container) {
    var root = createRoot(container);
    root.render(_jsx(ProductSearch, {}, void 0));
}
