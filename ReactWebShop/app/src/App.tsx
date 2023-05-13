import React from 'react';
import logo from './logo.svg';
import './App.css';
import DefaultHeader from './components/containers/default/DefaultHeader';
import { Route, Routes } from 'react-router-dom';
import CategoryCreatePage from './components/containers/default/category/create/CategoryCreatePage';

function App() {
  return (
    <>
      <DefaultHeader/>
      <Routes>
        <Route path='categories/create' element={<CategoryCreatePage/>}/>     
      </Routes>
    </>
  );
}

export default App;
