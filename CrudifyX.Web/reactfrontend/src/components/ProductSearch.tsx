// ProductSearch.tsx
import React, { useEffect, useState } from 'react';

interface Product {
    id: number;
    name: string;
    price: number;
    quantity: number;
}

export const ProductSearch: React.FC = () => {
    const [products, setProducts] = useState<Product[]>([]);
    const [query, setQuery] = useState("");
    const [newProduct, setNewProduct] = useState<Omit<Product, 'id'>>({
        name: '',
        price: 0,
        quantity: 0
    });

    const [editProduct, setEditProduct] = useState<Product | null>(null);

    const fetchProducts = async () => {
        const res = await fetch('/api/Product/GetAll');
        const data = await res.json();
        setProducts(data);
    };

    useEffect(() => {
        fetchProducts();
    }, []);

    const handleCreate = async () => {
        const res = await fetch('/api/Product/Create', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(newProduct)
        });
        if (res.ok) {
            await fetchProducts();
            setNewProduct({ name: '', price: 0, quantity: 0 });
        }
    };

    const handleEdit = async () => {
        if (!editProduct) return;
        const res = await fetch(`/api/Product/Edit/${editProduct.id}`, {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(editProduct)
        });
        if (res.ok) {
            setEditProduct(null);
            fetchProducts();
        }
    };

    const handleDelete = async (id: number) => {
        if (!window.confirm('Are you sure you want to delete this product?')) return;
        const res = await fetch(`/api/Product/Delete/${id}`, { method: 'DELETE' });
        if (res.ok) fetchProducts();
    };

    const filtered = products.filter(p =>
        p.name.toLowerCase().includes(query.toLowerCase())
    );

    return (
        <div className="container mt-4">
            <h2>Product Search</h2>

            <div className="input-group mb-3">
                <input
                    className="form-control"
                    placeholder="Search by product name..."
                    value={query}
                    onChange={(e) => setQuery(e.target.value)}
                />
            </div>

            {/*<div className="mb-4">*/}
            {/*    <h4>Add New Product</h4>*/}
            {/*    <input*/}
            {/*        className="form-control mb-2"*/}
            {/*        placeholder="Name"*/}
            {/*        value={newProduct.name}*/}
            {/*        onChange={(e) => setNewProduct({ ...newProduct, name: e.target.value })}*/}
            {/*    />*/}
            {/*    <input*/}
            {/*        type="number"*/}
            {/*        className="form-control mb-2"*/}
            {/*        placeholder="Price"*/}
            {/*        value={newProduct.price}*/}
            {/*        onChange={(e) => setNewProduct({ ...newProduct, price: parseFloat(e.target.value) })}*/}
            {/*    />*/}
            {/*    <input*/}
            {/*        type="number"*/}
            {/*        className="form-control mb-2"*/}
            {/*        placeholder="Quantity"*/}
            {/*        value={newProduct.quantity}*/}
            {/*        onChange={(e) => setNewProduct({ ...newProduct, quantity: parseInt(e.target.value) })}*/}
            {/*    />*/}
            {/*    <button className="btn btn-success" onClick={handleCreate}>Add Product</button>*/}
            {/*</div>*/}
            <div className="mb-4">
                <h4>Add New Product</h4>
                <div className="row g-2 align-items-center">
                    <div className="col-md-4 col-sm-12">
                        <input
                            className="form-control"
                            placeholder="Name"
                            value={newProduct.name}
                            onChange={(e) => setNewProduct({ ...newProduct, name: e.target.value })}
                        />
                    </div>
                    <div className="col-md-2 col-sm-6">
                        <input
                            type="number"
                            className="form-control"
                            placeholder="Price"
                            value={newProduct.price}
                            onChange={(e) => setNewProduct({ ...newProduct, price: parseFloat(e.target.value) })}
                        />
                    </div>
                    <div className="col-md-2 col-sm-6">
                        <input
                            type="number"
                            className="form-control"
                            placeholder="Quantity"
                            value={newProduct.quantity}
                            onChange={(e) => setNewProduct({ ...newProduct, quantity: parseInt(e.target.value) })}
                        />
                    </div>
                    <div className="col-md-2 col-sm-12">
                        <button className="btn btn-success w-100" onClick={handleCreate}>
                            Add Product
                        </button>
                    </div>
                </div>
            </div>



            {editProduct && (
                <div className="mb-4">
                    <h4>Edit Product</h4>
                    <input
                        className="form-control mb-2"
                        value={editProduct.name}
                        onChange={(e) => setEditProduct({ ...editProduct, name: e.target.value })}
                    />
                    <input
                        type="number"
                        className="form-control mb-2"
                        value={editProduct.price}
                        onChange={(e) => setEditProduct({ ...editProduct, price: parseFloat(e.target.value) })}
                    />
                    <input
                        type="number"
                        className="form-control mb-2"
                        value={editProduct.quantity}
                        onChange={(e) => setEditProduct({ ...editProduct, quantity: parseInt(e.target.value) })}
                    />
                    <button className="btn btn-primary me-2" onClick={handleEdit}>Save</button>
                    <button className="btn btn-secondary" onClick={() => setEditProduct(null)}>Cancel</button>
                </div>
            )}

            <table className="table table-striped table-bordered">
                <thead>
                    <tr><th>Name</th><th>Price</th><th>Quantity</th><th>Actions</th></tr>
                </thead>
                <tbody>
                    {filtered.map(p => (
                        <tr key={p.id}>
                            <td>{p.name}</td>
                            <td>${p.price.toFixed(2)}</td>
                            <td>{p.quantity}</td>
                            <td>
                                <button className="btn btn-sm btn-warning me-2" onClick={() => setEditProduct(p)}>Edit</button>
                                <button className="btn btn-sm btn-danger" onClick={() => handleDelete(p.id)}>Delete</button>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
};
