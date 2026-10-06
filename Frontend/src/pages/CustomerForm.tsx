import { useNavigate, useLocation } from 'react-router-dom';
import { useFormik } from 'formik';
import * as Yup from 'yup';
import Grid from '@mui/material/Grid';
import {
    TextField, Button, Typography, Container, Box, Paper,
    FormControl, InputLabel, Select, MenuItem
} from '@mui/material';
import { useDispatch } from 'react-redux';
import type { AppDispatch } from '../features/store';
import { addCustomer } from '../features/customerSlice';
import { updateCustomer } from '../features/customerSlice';
import { fetchCustomers } from '../features/customerSlice';

const validationSchema = Yup.object({
    MusteriAdi: Yup.string().required('Müşteri adı zorunludur'),
    MusteriSoyadi: Yup.string().required('Müşteri soyadı zorunludur'),
    Tc_No: Yup.string()
        .matches(/^[0-9]+$/, 'T.C. Kimlik Numarası sadece sayılardan oluşmalıdır')
        .length(11, 'T.C. Kimlik Numarası tam 11 haneli olmalıdır')
        .required('T.C. Kimlik Numarası zorunludur'),
    Email: Yup.string().email('Geçerli bir e-posta adresi giriniz').required('E-posta adresi zorunludur'),
    DogumTarihi: Yup.date()
        .max(new Date(), 'Doğum tarihi bugünden sonrası olamaz')
        .required('Doğum tarihi zorunludur'),
    AnneKızlıkSoyadı: Yup.string().required('Anne kızlık soyadı zorunludur'),
    Cinsiyet: Yup.string().required('Cinsiyet seçimi zorunludur'),
    SigortaDurumu: Yup.string().required('Sigorta durumu seçilmelidir'),
    MusteriDurumu: Yup.string().required('Müşteri durumu seçilmelidir'),
});

export default function CustomerForm() {
    const navigate = useNavigate();
    const dispatch = useDispatch<AppDispatch>();
    const location = useLocation();

    const musteriToEdit = location.state?.musteri;

    const formik = useFormik({
        enableReinitialize: true,
        initialValues: {
            MusteriAdi: musteriToEdit ? musteriToEdit.MusteriAdi : '',
            MusteriSoyadi: musteriToEdit ? musteriToEdit.MusteriSoyadi : '',
            Tc_No: musteriToEdit ? musteriToEdit.Tc_No : '',
            DogumTarihi: musteriToEdit && musteriToEdit.DogumTarihi ? musteriToEdit.DogumTarihi.split('T')[0] : '',
            AnneKızlıkSoyadı: musteriToEdit ? musteriToEdit.AnneKızlıkSoyadı : '',
            Cinsiyet: musteriToEdit ? musteriToEdit.Cinsiyet : '',
            SigortaDurumu: musteriToEdit ? musteriToEdit.SigortaDurumu : '',
            MusteriDurumu: musteriToEdit ? musteriToEdit.MusteriDurumu : '',

            Email: musteriToEdit?.Email?.[0]?.EmailAdresi || '',
            Telefon_No: musteriToEdit?.Telefonlar?.[0]?.Numara || '',
            Telefon_Tipi: musteriToEdit?.Telefonlar?.[0]?.TelefonTipiID || 'M',
            Sehir: musteriToEdit?.Adresler?.[0]?.Il || '',
            Ilce: musteriToEdit?.Adresler?.[0]?.Ilce || '',
            AdresDetay: musteriToEdit?.Adresler?.[0]?.AdresDetay || '',
            AdresTipi: musteriToEdit?.Adresler?.[0]?.AdresTipiID || 'E',
        },
        validationSchema: validationSchema,
        onSubmit: async (values, { resetForm }) => {
            try {
                const payload = {
                    MusteriID: musteriToEdit ? musteriToEdit.MusteriID : 0,
                    MusteriAdi: values.MusteriAdi,
                    MusteriSoyadi: values.MusteriSoyadi,
                    Tc_No: values.Tc_No,
                    DogumTarihi: values.DogumTarihi,

                    // KİLİT ÇÖZÜM: C# tarafındaki DTO'larda harf farklılığı olduğu için
                    // (Kayıt DTO'su "i" ile, Güncelleme DTO'su "ı" ile istiyor)
                    // İkisini de gönderiyoruz, C# hangisine ihtiyacı varsa onu alacak, diğerini yok sayacak!
                    AnneKizlikSoyadi: values.AnneKızlıkSoyadı,
                    AnneKızlıkSoyadı: values.AnneKızlıkSoyadı,

                    Cinsiyet: values.Cinsiyet,
                    SigortaDurumu: values.SigortaDurumu,
                    MusteriDurumu: values.MusteriDurumu,
                    KayıtTarihi: musteriToEdit ? musteriToEdit.KayıtTarihi : new Date().toISOString(),
                    GuncellemeTarihi: new Date().toISOString(),

                    Emailler: [{ EmailAdresi: values.Email }],
                    Telefonlar: [{ Telefon_No: values.Telefon_No, telefon_Tipi: values.Telefon_Tipi }],
                    Adresler: [{
                        Sehir: values.Sehir,
                        // Aynı şekilde hem "i" hem "ı" ile gönderiyoruz
                        Ilce: values.Ilce,
                        İlce: values.Ilce,
                        AdresDetay: values.AdresDetay,
                        AdresTipi: values.AdresTipi
                    }]
                };

                if (musteriToEdit) {
                    await dispatch(updateCustomer(payload)).unwrap();
                    alert("Müşteri başarıyla güncellendi!");
                } else {
                    await dispatch(addCustomer(payload)).unwrap();
                    alert("Müşteri başarıyla eklendi!");
                }

                // Listeyi zorla güncelle (unwrap kullanmadan, en güvenli haliyle)
                await dispatch(fetchCustomers());

                resetForm();
                navigate("/list");
            } catch (error: any) {
                console.error("Sunucudan gelen detaylı hata:", error);
                alert("İşlem başarısız oldu:\n\n" + (error?.Message || JSON.stringify(error, null, 2)));
            }
        },
    });

    return (
        <Container maxWidth="md" sx={{ mt: 5, mb: 5 }}>
            <Paper elevation={4} sx={{ p: 4, borderRadius: 2 }}>
                <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', mb: 4 }}>
                    <Typography variant="h5" color="primary" sx={{ fontWeight: 'bold' }}>
                        {musteriToEdit ? "Müşteri Düzenle" : "Yeni Müşteri Kaydı"}
                    </Typography>
                    <Button variant="text" onClick={() => navigate("/")}>
                        İptal Et
                    </Button>
                </Box>

                <form onSubmit={formik.handleSubmit}>
                    <Grid container spacing={3}>
                        <Grid size={{ xs: 12, sm: 6 }}>
                            <TextField fullWidth id="MusteriAdi" name="MusteriAdi" label="Müşteri Adı" value={formik.values.MusteriAdi} onChange={formik.handleChange} onBlur={formik.handleBlur} error={formik.touched.MusteriAdi && Boolean(formik.errors.MusteriAdi)} helperText={formik.touched.MusteriAdi && formik.errors.MusteriAdi ? String(formik.errors.MusteriAdi) : undefined} />
                        </Grid>

                        <Grid size={{ xs: 12, sm: 6 }}>
                            <TextField fullWidth id="MusteriSoyadi" name="MusteriSoyadi" label="Müşteri Soyadı" value={formik.values.MusteriSoyadi} onChange={formik.handleChange} onBlur={formik.handleBlur} error={formik.touched.MusteriSoyadi && Boolean(formik.errors.MusteriSoyadi)} helperText={formik.touched.MusteriSoyadi && formik.errors.MusteriSoyadi ? String(formik.errors.MusteriSoyadi) : undefined} />
                        </Grid>

                        <Grid size={{ xs: 12, sm: 6 }}>
                            <TextField fullWidth id="Tc_No" name="Tc_No" label="T.C. Kimlik Numarası" value={formik.values.Tc_No} onChange={formik.handleChange} onBlur={formik.handleBlur} error={formik.touched.Tc_No && Boolean(formik.errors.Tc_No)} helperText={formik.touched.Tc_No && formik.errors.Tc_No ? String(formik.errors.Tc_No) : undefined} />
                        </Grid>

                        <Grid size={{ xs: 12, sm: 6 }}>
                            <TextField fullWidth id="Email" name="Email" label="E-Posta Adresi" value={formik.values.Email} onChange={formik.handleChange} onBlur={formik.handleBlur} error={formik.touched.Email && Boolean(formik.errors.Email)} helperText={formik.touched.Email && formik.errors.Email ? String(formik.errors.Email) : undefined} />
                        </Grid>

                        <Grid size={{ xs: 12, sm: 6 }}>
                            <TextField fullWidth id="DogumTarihi" name="DogumTarihi" label="Doğum Tarihi" type="date" slotProps={{ inputLabel: { shrink: true }, htmlInput: { max: new Date().toISOString().split("T")[0] } }} value={formik.values.DogumTarihi} onChange={formik.handleChange} onBlur={formik.handleBlur} error={formik.touched.DogumTarihi && Boolean(formik.errors.DogumTarihi)} helperText={formik.touched.DogumTarihi && formik.errors.DogumTarihi ? String(formik.errors.DogumTarihi) : undefined} />
                        </Grid>

                        <Grid size={{ xs: 12, sm: 6 }}>
                            <TextField fullWidth id="AnneKızlıkSoyadı" name="AnneKızlıkSoyadı" label="Anne Kızlık Soyadı" value={formik.values.AnneKızlıkSoyadı} onChange={formik.handleChange} onBlur={formik.handleBlur} error={formik.touched.AnneKızlıkSoyadı && Boolean(formik.errors.AnneKızlıkSoyadı)} helperText={formik.touched.AnneKızlıkSoyadı && formik.errors.AnneKızlıkSoyadı ? String(formik.errors.AnneKızlıkSoyadı) : undefined} />
                        </Grid>

                        <Grid size={{ xs: 12, sm: 4 }}>
                            <TextField fullWidth select id="Cinsiyet" name="Cinsiyet" label="Cinsiyet" value={formik.values.Cinsiyet} onChange={formik.handleChange} onBlur={formik.handleBlur} error={formik.touched.Cinsiyet && Boolean(formik.errors.Cinsiyet)} helperText={formik.touched.Cinsiyet && formik.errors.Cinsiyet ? String(formik.errors.Cinsiyet) : undefined} >
                                <MenuItem value="E">Erkek (E)</MenuItem>
                                <MenuItem value="K">Kadın (K)</MenuItem>
                            </TextField>
                        </Grid>

                        <Grid size={{ xs: 12, sm: 4 }}>
                            <TextField fullWidth select id="SigortaDurumu" name="SigortaDurumu" label="Sigorta Durumu" value={formik.values.SigortaDurumu} onChange={formik.handleChange} onBlur={formik.handleBlur} error={formik.touched.SigortaDurumu && Boolean(formik.errors.SigortaDurumu)} helperText={formik.touched.SigortaDurumu && formik.errors.SigortaDurumu ? String(formik.errors.SigortaDurumu) : undefined} >
                                <MenuItem value="4A">4A (SSK)</MenuItem>
                                <MenuItem value="4B">4B (Bağ-Kur)</MenuItem>
                                <MenuItem value="4C">4C (Emekli Sandığı)</MenuItem>
                            </TextField>
                        </Grid>

                        <Grid size={{ xs: 12, sm: 4 }}>
                            <TextField fullWidth select id="MusteriDurumu" name="MusteriDurumu" label="Müşteri Durumu" value={formik.values.MusteriDurumu} onChange={formik.handleChange} onBlur={formik.handleBlur} error={formik.touched.MusteriDurumu && Boolean(formik.errors.MusteriDurumu)} helperText={formik.touched.MusteriDurumu && formik.errors.MusteriDurumu ? String(formik.errors.MusteriDurumu) : undefined} >
                                <MenuItem value="A">Aktif (A)</MenuItem>
                                <MenuItem value="P">Pasif (P)</MenuItem>
                            </TextField>
                        </Grid>

                        <Grid size={{ xs: 12, sm: 4 }}>
                            <TextField fullWidth id="Telefon_No" name="Telefon_No" label="Telefon Numarası" value={formik.values.Telefon_No} onChange={formik.handleChange} />
                        </Grid>

                        <Grid size={{ xs: 12, sm: 2 }}>
                            <FormControl fullWidth>
                                <InputLabel id="telefon-tipi-label">Tel Tipi</InputLabel>
                                <Select labelId="telefon-tipi-label" id="Telefon_Tipi" name="Telefon_Tipi" label="Tip" value={formik.values.Telefon_Tipi} onChange={formik.handleChange} >
                                    <MenuItem value="M">Mobil</MenuItem>
                                    <MenuItem value="H">Ev</MenuItem>
                                    <MenuItem value="W">İş</MenuItem>
                                </Select>
                            </FormControl>
                        </Grid>

                        <Grid size={{ xs: 12, sm: 6 }}>
                            <TextField fullWidth id="Sehir" name="Sehir" label="Şehir" value={formik.values.Sehir} onChange={formik.handleChange} />
                        </Grid>

                        <Grid size={{ xs: 12, sm: 6 }}>
                            <TextField fullWidth id="Ilce" name="Ilce" label="İlçe" value={formik.values.Ilce} onChange={formik.handleChange} />
                        </Grid>

                        <Grid size={{ xs: 12, sm: 9 }}>
                            <TextField fullWidth multiline rows={2} id="AdresDetay" name="AdresDetay" label="Açık Adres" value={formik.values.AdresDetay} onChange={formik.handleChange} />
                        </Grid>

                        <Grid size={{ xs: 12, sm: 3 }}>
                            <FormControl fullWidth>
                                <InputLabel id="adres-tipi-label">Adres Tipi</InputLabel>
                                <Select labelId="adres-tipi-label" id="AdresTipi" name="AdresTipi" label="Tip" value={formik.values.AdresTipi} onChange={formik.handleChange} >
                                    <MenuItem value="E">Ev</MenuItem>
                                    <MenuItem value="I">İş</MenuItem>
                                </Select>
                            </FormControl>
                        </Grid>

                        <Grid size={{ xs: 12 }}>
                            <Button color="primary" variant="contained" fullWidth type="submit" size="large" sx={{ mt: 2 }}>
                                {musteriToEdit ? "Güncelle" : "Kaydet"}
                            </Button>
                        </Grid>
                    </Grid>
                </form>
            </Paper>
        </Container>
    );
}