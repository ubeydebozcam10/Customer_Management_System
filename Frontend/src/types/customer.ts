
// Backend tarafındaki DTO ve nesnelerin ts karşılıkları

export interface Musteri {
    MusteriID: number;
    MusteriAdi: string;
    MusteriSoyadi: string;
    Tc_No: string;
    Email?: { EmailAdresi: string }[];
    Adresler?: { Il: string; Ilce: string; AdresDetay: string }[];
    Telefonlar?: { Numara: string }[];
    DogumTarihi: string;
    AnneKızlıkSoyadı: string;
    Cinsiyet: string;
    SigortaDurumu: string;
    MusteriDurumu: string;
    KayıtTarihi: string;
    GuncellemeTarihi: string;
}