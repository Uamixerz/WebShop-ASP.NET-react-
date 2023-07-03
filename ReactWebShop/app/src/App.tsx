import './App.css';
import DefaultHeader from './components/containers/default/DefaultHeader';
import { Route, Routes } from 'react-router-dom';
import CategoryCreatePage from './components/containers/category/create/CategoryCreatePage';
import RegisterPage from './components/containers/auth/register/RegisterPage';
import LoginPage from './components/containers/auth/login/LoginPage';
import CategoryEditPage from './components/containers/category/edit/CategoryEditPage';
import AdminPage from './components/containers/admin/AdminPage';
import AdminLayout from './components/containers/admin/container/AdminLayout';
import CategoryListPage from './components/containers/category/list/categoryListPage';
import ForbiddenPage from './components/containers/default/ForbiddenPage';
import ProductListPage from './components/containers/product/list/ProductListPage';
import ProductCreatePage from './components/containers/product/create/ProductCreatePage';


function App() {
  return (
    <>
      <DefaultHeader />
      <Routes>
        <Route path='403' element={<ForbiddenPage />} />
        <Route path='login' element={<LoginPage />} />
        <Route path='register' element={<RegisterPage />} />

        <Route path='admin' element={<AdminLayout />}>
          <Route path='categories'>
            <Route path='edit/:id' element={<CategoryEditPage />} />
            <Route path='create' element={<CategoryCreatePage />} />
            <Route path='list' element={<CategoryListPage />} />
          </Route>
          <Route path={"products"}>
            <Route index element={<ProductListPage />} />
            <Route path='create' element={<ProductCreatePage />} />
          </Route>
        </Route>



      </Routes>
    </>
  );
}

export default App;
