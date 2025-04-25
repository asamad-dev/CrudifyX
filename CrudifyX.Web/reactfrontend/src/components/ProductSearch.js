var __assign = (this && this.__assign) || function () {
    __assign = Object.assign || function(t) {
        for (var s, i = 1, n = arguments.length; i < n; i++) {
            s = arguments[i];
            for (var p in s) if (Object.prototype.hasOwnProperty.call(s, p))
                t[p] = s[p];
        }
        return t;
    };
    return __assign.apply(this, arguments);
};
import { jsx as _jsx, jsxs as _jsxs } from "react/jsx-runtime";
import { useEffect, useState } from 'react';
export var ProductSearch = function () {
    var _a = useState([]), products = _a[0], setProducts = _a[1];
    var _b = useState(""), query = _b[0], setQuery = _b[1];
    useEffect(function () {
        fetch("/api/Product/GetAll")
            .then(function (res) { return res.json(); })
            .then(function (data) { return setProducts(data); });
    }, []);
    var filtered = products.filter(function (p) {
        return p.name.toLowerCase().includes(query.toLowerCase());
    });
    return (_jsxs("div", __assign({ className: "container mt-4" }, { children: [_jsx("input", { className: "form-control mb-3", type: "text", placeholder: "Search by product name...", value: query, onChange: function (e) { return setQuery(e.target.value); } }, void 0), _jsxs("table", __assign({ className: "table table-bordered" }, { children: [_jsx("thead", { children: _jsxs("tr", { children: [_jsx("th", { children: "Name" }, void 0), _jsx("th", { children: "Price" }, void 0), _jsx("th", { children: "Quantity" }, void 0)] }, void 0) }, void 0), _jsx("tbody", { children: filtered.map(function (p) { return (_jsxs("tr", { children: [_jsx("td", { children: p.name }, void 0), _jsx("td", { children: p.price }, void 0), _jsx("td", { children: p.quantity }, void 0)] }, p.id)); }) }, void 0)] }), void 0)] }), void 0));
};
