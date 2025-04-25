import React, { useEffect, useState } from 'react';

type Product = {
    id: number;
    name: string;
    price: number;
    quantity: number;
};

export const ProductSearch = () => {
    const [products, setProducts] = useState<Product[]>([]);
    const [query, setQuery] = useState("");

    useEffect(() => {
        fetch("/api/Product/GetAll")
            .then(res => res.json())
            .then(data => setProducts(data));
    }, []);

    const filtered = products.filter(p =>
        p.name.toLowerCase().includes(query.toLowerCase())
    );

    return (
        <div className="container mt-4">
            <input
                className="form-control mb-3"
                type="text"
                placeholder="Search by product name..."
                value={query}
                onChange={e => setQuery(e.target.value)}
            />
            <table className="table table-bordered">
                <thead>
                    <tr><th>Name</th><th>Price</th><th>Quantity</th></tr>
                </thead>
                <tbody>
                    {filtered.map(p => (
                        <tr key={p.id}>
                            <td>{p.name}</td>
                            <td>{p.price}</td>
                            <td>{p.quantity}</td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
};
