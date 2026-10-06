// Yönlendirdiğimiz ana ekranları temsil eder.

import { useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { fetchCustomers } from '../features/customerSlice';
import type { RootState, AppDispatch } from '../features/store';
import { Table, TableBody, TableCell, TableHead, TableRow, TableContainer, Paper, Container, Typography, Box, Button } from '@mui/material';
import { useNavigate } from 'react-router-dom';
import { deleteCustomer } from '../features/customerSlice';



export default function CustomerList() {
    const dispatch = useDispatch<AppDispatch>();
    const navigate = useNavigate();
    const { customers, loading, error } = useSelector((state: RootState) => state.customer);

    useEffect(() => {
        dispatch(fetchCustomers());
    }, [dispatch]);

    if (loading) return <div>Veriler yükleniyor...</div>;
    if (error) return <div>Hata oluştu: {error}</div>;

    console.log("API'den Gelen Müşteri Listesi:", customers);

    return (
        <Container maxWidth="xl" sx={{ mt: 5, mb: 5 }}>
            <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', mb: 3 }}>
                <Typography variant="h4" color="primary" sx={{ fontWeight: 'bold' }}>
                    Müşteri Listesi
                </Typography>
                <Button variant="outlined" onClick={() => navigate("/")}>
                    Anasayfaya Dön
                </Button>
            </Box>

            <TableContainer
                component={Paper}
                elevation={4}
                sx={{
                    // --- ÖZEL SCROLLBAR TASARIMI ---
                    '&::-webkit-scrollbar': {
                        height: '10px', // Yatay kaydırma çubuğunun kalınlığı
                        width: '10px',  // Dikey kaydırma çubuğunun kalınlığı
                    },
                    '&::-webkit-scrollbar-track': {
                        backgroundColor: '#f4f6f8', // Çubuğun arka plan (ray) rengi
                        borderRadius: '10px',
                    },
                    '&::-webkit-scrollbar-thumb': {
                        backgroundColor: '#b0b8c4', // Kayan çubuğun kendi rengi
                        borderRadius: '10px',
                        border: '2px solid #f4f6f8', // Çubuğun etrafına hafif boşluk hissi verir
                        '&:hover': {
                            backgroundColor: '#8792a1', // Fareyle üzerine gelince biraz koyulaşır
                        },
                    },
                }}
            >
                <Table sx={{ minWidth: 650 }}>
                    <TableHead sx={{ backgroundColor: '#f4f6f8' }}>
                        <TableRow>
                            <TableCell sx={{ fontWeight: 'bold' }}>ID</TableCell>
                            <TableCell sx={{ fontWeight: 'bold' }}>Adı</TableCell>
                            <TableCell sx={{ fontWeight: 'bold' }}>Soyadı</TableCell>
                            <TableCell sx={{ fontWeight: 'bold' }}>E-Posta</TableCell>
                            <TableCell sx={{ fontWeight: 'bold' }}>Telefon</TableCell>
                            <TableCell sx={{ fontWeight: 'bold' }}>Adres</TableCell>
                            <TableCell sx={{ fontWeight: 'bold' }}>Doğum Tarihi</TableCell>
                            <TableCell sx={{ fontWeight: 'bold' }}>TC No</TableCell>
                            <TableCell sx={{ fontWeight: 'bold' }}>Sigorta</TableCell>
                            <TableCell sx={{ fontWeight: 'bold' }}>Anne Kızlık Soyadı</TableCell>
                            <TableCell sx={{ fontWeight: 'bold' }}>Cinsiyet</TableCell>
                            <TableCell sx={{ fontWeight: 'bold' }}>Durum</TableCell>
                            <TableCell sx={{ fontWeight: 'bold' }}>Kayıt Tarihi</TableCell>
                            <TableCell sx={{ fontWeight: 'bold' }}>Güncelleme Tarihi</TableCell>
                            {/* YENİ EKLENEN SÜTUN */}
                            <TableCell sx={{ fontWeight: 'bold' }}>İşlemler</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {customers.map((customer) => (
                            <TableRow key={customer.MusteriID} hover>
                                <TableCell>{customer.MusteriID}</TableCell>
                                <TableCell>{customer.MusteriAdi}</TableCell>
                                <TableCell>{customer.MusteriSoyadi}</TableCell>
                                <TableCell>
                                    {customer.Email && customer.Email.length > 0 ? (
                                        customer.Email.map((e, index) => (
                                            <div key={index}>{e.EmailAdresi}</div>
                                        ))
                                    ) : "Yok"}
                                </TableCell>

                                <TableCell>
                                    {customer.Telefonlar && customer.Telefonlar.length > 0 ? (
                                        customer.Telefonlar.map((t, index) => (
                                            <div key={index}>{t.Numara}</div>
                                        ))
                                    ) : "Yok"}
                                </TableCell>

                                <TableCell>
                                    {customer.Adresler && customer.Adresler.length > 0 ? (
                                        customer.Adresler.map((a, index) => (
                                            <div key={index} style={{ marginBottom: '8px' }}>
                                                <strong>{a.Il} / {a.Ilce}</strong><br />
                                                <span style={{ fontSize: '0.85em', color: 'gray' }}>{a.AdresDetay}</span>
                                            </div>
                                        ))
                                    ) : "Yok"}
                                </TableCell>
                                <TableCell>{new Date(customer.DogumTarihi).toLocaleDateString('tr-TR')}</TableCell>
                                <TableCell>{customer.Tc_No}</TableCell>
                                <TableCell>{customer.SigortaDurumu}</TableCell>
                                <TableCell>{customer.AnneKızlıkSoyadı}</TableCell>
                                <TableCell>{customer.Cinsiyet}</TableCell>
                                <TableCell>{customer.MusteriDurumu}</TableCell>
                                <TableCell>{new Date(customer.KayıtTarihi).toLocaleDateString('tr-TR')}</TableCell>
                                <TableCell>{new Date(customer.GuncellemeTarihi).toLocaleDateString('tr-TR')}</TableCell>

                                {/* YENİ EKLENEN BUTON HÜCRESİ */}
                                <TableCell>
                                    {/* display: 'flex' ve gap kullanarak butonları yan yana şıkça hizalıyoruz */}
                                    <div style={{ display: 'flex', gap: '8px' }}>
                                        <Button
                                            variant="contained"
                                            color="primary"
                                            size="small"
                                            onClick={() => navigate('/form', { state: { musteri: customer } })}
                                        >
                                            GÜNCELLE
                                        </Button>

                                        <Button
                                            variant="contained"
                                            color="error"
                                            size="small"
                                            onClick={async () => {
                                                if (window.confirm(`${customer.MusteriAdi} isimli müşteriyi silmek istediğinize emin misiniz?`)) {
                                                    try {
                                                        await dispatch(deleteCustomer(customer.MusteriID)).unwrap();
                                                        alert("Müşteri başarıyla silindi!");
                                                    } catch (err) {
                                                        alert("Silme işlemi başarısız oldu.");
                                                    }
                                                }
                                            }}
                                        >
                                            SİL
                                        </Button>
                                    </div>
                                </TableCell>
                            </TableRow>
                        ))}
                    </TableBody>
                </Table>
            </TableContainer>

            <Typography sx={{ mt: 2, color: 'textSecondary', textAlign: 'right' }}>
                Toplam Müşteri Sayısı: {customers.length}
            </Typography>
        </Container>
    );
}