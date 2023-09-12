
import { Routes, Route } from 'react-router-dom';
import CategoryCreatePage from './components/containers/category/create/CategoryCreatePage';
import RegisterPage from './components/containers/auth/register/RegisterPage';
import LoginPage from './components/containers/auth/login/LoginPage';
import CategoryEditPage from './components/containers/category/edit/CategoryEditPage';
import AdminLayout from './components/containers/admin/container/AdminLayout';
import CategoryListPage from './components/containers/category/list/CategoryListPage';
import ForbiddenPage from './components/containers/default/ForbiddenPage';
import ProductListPage from './components/containers/product/list/ProductListPage';
import ProductCreatePage from './components/containers/product/create/ProductCreatePage';
import './App.css'
import DefaultHeader from './components/containers/default/DefaultHeader';
import HomePage from './components/home/HomePage';

import ProductBuyPage from './components/containers/product/buy/ProductBuyPage';
import CharacteristicListPage from './components/containers/characteristic/list/CharacteristicListPage';
import CharacteristicCreatePage from './components/containers/characteristic/create/CharacteristicCreatePage';

function App() {

  return (
    <>
      <DefaultHeader />
        <Routes>
          <Route path='403' element={<ForbiddenPage />} />
          <Route path='login' element={<LoginPage />} />
          <Route path='register' element={<RegisterPage />} />
          <Route index element={<HomePage />}/>

          <Route path={"products"}>
              <Route index element={<ProductListPage />} />
              <Route path='create' element={<ProductCreatePage />} />
              
              <Route path='buy/:id' element={<ProductBuyPage />} />
          </Route>

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
            <Route path={"characteristic"}>
              <Route index element={<CharacteristicListPage />} />
              <Route path='create' element={<CharacteristicCreatePage />} />
            </Route>
          </Route>
        </Routes>
      
    </>
  )
}

export default App
